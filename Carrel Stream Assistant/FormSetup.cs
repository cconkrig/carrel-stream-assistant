using System;
using System.Data.SQLite;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Drawing;
using System.Collections.Generic;

namespace Carrel_Stream_Assistant
{
    public partial class FormSetup : Form
    {

        //Reference the Parent Form
        private readonly MainForm parentForm;
        #pragma warning disable IDE0044 // Disable "Make field readonly" suggestion
        private MaskedTextBox maskedTextBox = new MaskedTextBox();

        public FormSetup(MainForm parent)
        {
            InitializeComponent();
            txtNetCue.KeyPress += TxtNetCue_KeyPress;
            EnumerateInputDevices();
            EnumerateOutputDevices();

            // Hookup reference to parent
            parentForm = parent;

            // Subscribe to Events
            dgRots.CellBeginEdit += DgRots_CellBeginEdit;
            dgRots.CellEndEdit += DgRots_CellEndEdit;
            dgRots.SelectionChanged += DgRots_SelectionChanged;
            dgRots.KeyDown += DgRots_KeyDown;

        }
        //private string CalculatePercentage(int value)
        //{
        //    float percentage = (float)value / sliderInputVolume.Maximum * 100;
        //    return percentage.ToString("0.00"); // Format the percentage value
        //}

        private float ConvertLinearToDB(float linearValue)
        {
            //To change a linear value into dB levels, we need to use:
            // dB = 20 * log10(linearValue)
            if (linearValue == 0)
                return 0f;
            if (linearValue < 0)
                return -60.0f; // Minimum possible dB value (e.g., muted)

            return (float)(20 * Math.Log10(linearValue));
        }

        private void DgRots_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            maskedTextBox.Mask = "####-##-## ##:##";
            Rectangle rect = dgRots.GetCellDisplayRectangle(e.ColumnIndex,e.RowIndex, true);
            maskedTextBox.Location = rect.Location;
            maskedTextBox.Size = rect.Size;
            maskedTextBox.Text = "";

            if(dgRots[e.ColumnIndex, e.RowIndex].Value != null)
            {
                maskedTextBox.Text = dgRots[e.ColumnIndex, e.RowIndex].Value.ToString();
            }
            maskedTextBox.Visible = true;
        }

        private void FormSetup_Load(object sender, EventArgs e)
        {
            LoadGeneralSettingsScreen();
            BtnSaveGeneral.Enabled = false;
            BtnCancelGeneral.Enabled = false;
            // Set up the ToolTip for the TrackBar
            float linearVolume = (float)sliderInputVolume.Value / sliderInputVolume.Maximum;
            float dB = ConvertLinearToDB(linearVolume);
            ttVolume.SetToolTip(sliderInputVolume, $"Volume: {dB:F2} dB");
        }
        private void EnumerateInputDevices()
        {
            cboAudioFeedInput.Items.Clear();
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            int inputCounter = 0;
            foreach (MMDevice device in enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active))
            {
                cboAudioFeedInput.Items.Add(new AudioDeviceInfo(device.FriendlyName, device.FriendlyName));
                inputCounter++;
            }
        }
        private void EnumerateOutputDevices()
        {
            cboAudioOutput.Items.Clear();
            var waveOutDevices1 = WaveOut.DeviceCount;
            for (int deviceNumber = 0; deviceNumber < waveOutDevices1; deviceNumber++)
            {
                var capabilities = WaveOut.GetCapabilities(deviceNumber);
                cboAudioOutput.Items.Add($"{capabilities.ProductName}");
            }
        }

        private void ResetNetCueForm(bool resetSelection)
        {
            lstNetcues.Enabled = true;
            if (resetSelection) { lstNetcues.SelectedIndex = -1; }

            btnNetCueAdd.Enabled = true;
            btnNetCueEdit.Enabled = false;
            btnNetCueDelete.Enabled = false;

            lblNetCue.Enabled = false;
            txtNetCue.Text = "";
            txtNetCue.Enabled = false;
            lblFriendlyName.Enabled = false;
            txtFriendlyNetCueName.Text = "";
            txtFriendlyNetCueName.Enabled = false;
            btnCancelNetCue.Enabled = false;
            btnSaveNetCue.Enabled = false;
            grpBoxNetCue.Enabled = false;

            btnRotAdd.Enabled = false;
            btnRotDelete.Enabled = false;
            cboRotNetCues.SelectedIndex = -1;
            dgRots.Rows.Clear();
            dgRots.Enabled = false;
        }

        private void BtnNetCueAdd_Click(object sender, EventArgs e)
        {
            grpBoxNetCue.Enabled = true;
            btnSaveNetCue.Enabled = true;
            btnCancelNetCue.Enabled = true;
            lblFriendlyName.Enabled = true;
            txtFriendlyNetCueName.Text = "";
            txtFriendlyNetCueName.Enabled = true;
            txtNetCue.Enabled = true;
            txtNetCue.Text = "";
            txtNetCue.Focus();

            lstNetcues.Enabled = false;
            lstNetcues.SelectedIndex = -1;

            btnNetCueAdd.Enabled = false;
            btnNetCueEdit.Enabled = false;
            btnNetCueDelete.Enabled = false;
            lblNetCue.Enabled = true;
        }

        private void BtnCancelNetCue_Click(object sender, EventArgs e)
        {
            ResetNetCueForm(true);
        }

        private void LoadNetCuesList()
        {
            lstNetcues.Items.Clear();
            cboRotNetCues.Items.Clear();
            using (SQLiteConnection connection = new SQLiteConnection(DatabaseOperations.connectionString))
            {
                connection.Open();
                // Read the inserted record
                string selectQuery = "SELECT Id, NetCue, FriendlyName FROM NetCues ORDER BY NetCue ASC";
                using (SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, connection))
                using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string netcue = reader.GetString(1);
                        string friendly = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        string netcueItem = netcue;
                        if (friendly != null && friendly != "")
                        {
                            netcueItem = friendly + $" ({netcue})";
                        }
                        var listItem = new NetCueItem
                        {
                            Id = id,
                            Text = netcueItem,
                            NetCue = netcue,
                            FriendlyName = friendly
                        };

                        lstNetcues.Items.Add(listItem);
                        cboRotNetCues.Items.Add(listItem);
                    }
                }
            }
        }

        private void BtnSaveNetCue_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            using (SQLiteConnection connection = new SQLiteConnection(DatabaseOperations.connectionString))
            {
                connection.Open();
                try
                {
                    if (lstNetcues.SelectedIndex != -1)
                    {
                        // Update an existing record
                        NetCueItem selectedItem = (NetCueItem)lstNetcues.SelectedItem;
                        int selectedId = selectedItem.Id;

                        string updateQuery = "UPDATE NetCues SET NetCue = @NetCue, FriendlyName = @FriendlyName WHERE Id = @Id";
                        using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@NetCue", txtNetCue.Text.ToUpper().Trim());
                            updateCommand.Parameters.AddWithValue("@FriendlyName", txtFriendlyNetCueName.Text.Trim());
                            updateCommand.Parameters.AddWithValue("@Id", selectedId);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Insert a record
                        string insertQuery = "INSERT INTO NetCues (NetCue, FriendlyName) VALUES (@NetCue, @FriendlyName)";
                        using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@NetCue", txtNetCue.Text.ToUpper().Trim());
                            insertCommand.Parameters.AddWithValue("@FriendlyName", txtFriendlyNetCueName.Text.Trim());
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
                catch (SQLiteException ex)
                {
                    if ((int)ex.ErrorCode == (int)SQLiteErrorCode.Constraint && ex.Message.Contains("UNIQUE"))
                    {
                        Cursor.Current = Cursors.Default;
                        // Handle unique constraint violation
                        MessageBox.Show("The NetCue value you entered already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        // Handle other exceptions
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            ResetNetCueForm(true);
            LoadNetCuesList();
            Cursor.Current = Cursors.Default;
        }

        private void TxtNetCue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Convert the pressed character to uppercase
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void LstNetcues_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstNetcues.SelectedItem != null)
            {
                NetCueItem selectedItem = (NetCueItem)lstNetcues.SelectedItem;
                string netCue = selectedItem.NetCue;
                string friendly = selectedItem.FriendlyName;
                ResetNetCueForm(false);
                btnNetCueAdd.Enabled = true;
                btnNetCueEdit.Enabled = true;
                btnNetCueDelete.Enabled = true;
                txtNetCue.Text = netCue;
                txtFriendlyNetCueName.Text = friendly;
                lstNetcues.Focus();
            }
        }

        private void BtnNetCueEdit_Click(object sender, EventArgs e)
        {
            btnCancelNetCue.Enabled = true;
            btnSaveNetCue.Enabled = true;
            grpBoxNetCue.Enabled = true;
            lblNetCue.Enabled = true;
            txtNetCue.Enabled = true;
            lblFriendlyName.Enabled = true;
            txtFriendlyNetCueName.Enabled = true;
            btnNetCueAdd.Enabled = false;
            btnNetCueDelete.Enabled = false;
            btnNetCueEdit.Enabled = false;
        }

        private void BtnNetCueDelete_Click(object sender, EventArgs e)
        {
            if (lstNetcues.SelectedItem != null && ConfirmDelete())
            {
                NetCueItem selectedItem = (NetCueItem)lstNetcues.SelectedItem;
                int selectedId = selectedItem.Id;

                using (SQLiteConnection connection = new SQLiteConnection(DatabaseOperations.connectionString))
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM NetCues WHERE Id = @Id";
                    using (SQLiteCommand deleteCommand = new SQLiteCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@Id", selectedId);
                        deleteCommand.ExecuteNonQuery();
                    }

                }
                ResetNetCueForm(true);
                LoadNetCuesList();
            }
        }

        private bool ConfirmDelete()
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this NetCue? It will be ignored going forward and any rotations associated with it will be deleted.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }

        private void CboRotNetCues_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Unsubscribe from the event temporarily
            CheckMuteNetCuePlayback.CheckedChanged -= CheckMuteNetCuePlayback_CheckedChanged;
            dgRots.Rows.Clear();
            cboRotType.Enabled = true;
            CheckMuteNetCuePlayback.Enabled = true;
            using (SQLiteConnection connection = new SQLiteConnection(DatabaseOperations.connectionString))
            {
                connection.Open();
                NetCueItem selectedItem = (NetCueItem)cboRotNetCues.SelectedItem;
                int selectedId = selectedItem.Id;

                string selectQuery = "SELECT Type, Muted FROM NetCues WHERE Id = @Id";
                using (SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@Id", selectedId);
                    using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string rottype = reader.GetString(0); // Assuming 'Type' is in the first column
                            int mutedValue = reader.GetInt32(1); // Assuming 'Muted' is in the second column

                            // Set the checkbox's text color based on the 'Muted' value
                            CheckMuteNetCuePlayback.ForeColor = mutedValue == 1 ? Color.Green : Color.Red;

                            // Update the checkbox's checked state based on the 'Muted' value
                            CheckMuteNetCuePlayback.Checked = mutedValue == 1;

                            if (rottype == "simple")
                            {
                                cboRotType.SelectedIndex = 0;
                            }
                        }
                    }
                }
                
                // Re-subscribe to the checkmark event.
                CheckMuteNetCuePlayback.CheckedChanged += CheckMuteNetCuePlayback_CheckedChanged;

                selectQuery = "SELECT Id, NetCueID, SortOrder, Marker, CartPath, StartDate, EndDate FROM Rotations WHERE NetCueID = @NetCueID ORDER BY SortOrder";
                using (SQLiteCommand rotSelectCommand = new SQLiteCommand(selectQuery, connection))
                {
                    rotSelectCommand.Parameters.AddWithValue("@NetCueID", selectedId);
                    using (SQLiteDataReader reader = rotSelectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            int netCueID = reader.GetInt32(1);
                            int sortOrder = reader.GetInt32(2);
                            int marker = reader.GetInt32(3);
                            string path = string.Empty; // Initialize to an empty string by default
                            if (!reader.IsDBNull(4))   // Check if the column value is not NULL
                            {
                                path = reader.GetString(4); // Only read the string value if it's not NULL
                            }
                            string startDate = string.Empty; // Initialize to an empty string by default
                            if (!reader.IsDBNull(5))   // Check if the column value is not NULL
                            {
                                startDate = reader.GetString(5); // Only read the string value if it's not NULL
                            }
                            string endDate = string.Empty; // Initialize to an empty string by default
                            if (!reader.IsDBNull(6))   // Check if the column value is not NULL
                            {
                                endDate = reader.GetString(6); // Only read the string value if it's not NULL
                            }

                            RotationItem rotationItem = new RotationItem
                            {
                                Id = id,
                                NetCueID = netCueID,
                                SortOrder = sortOrder,
                                Marker = marker,
                                Path = path,
                                CartName = Path.GetFileName(path),
                                StartDate = startDate,
                                EndDate = endDate
                            };

                            PopulateRotationDataGridView(dgRots, rotationItem);
                        }
                    }
                }
            }
            btnRotAdd.Enabled = true;
            dgRots.Enabled = true;
            if(dgRots.Rows.Count > 0)
            {
                btnRotDelete.Enabled = true;
                dgRots.Focus();
            }
        }

        private void PopulateRotationDataGridView(DataGridView dataGridView, RotationItem rotationItem)
        {
            int rowIndex = dataGridView.Rows.Add();
            DataGridViewRow row = dataGridView.Rows[rowIndex];

            if (rotationItem.Marker == 1)
            {
                row.Cells[0].Value = ">";
            }
            else
            {
                row.Cells[0].Value = "";
            }

            row.Cells[1].Value = rotationItem.CartName;
            row.Cells[2].Value = rotationItem.StartDate;
            row.Cells[3].Value = rotationItem.EndDate;
            row.Cells[4].Value = rotationItem.Path;
            row.Tag = rotationItem;

            // Set the Id property of the rotationItem to 0 for new insertions
            rotationItem.Id = rotationItem.Id;
        }

        private void PopulateReelToReelDataGridView(DataGridView dataGridView, ReelItem reelItem)
        {
            int rowIndex = dataGridView.Rows.Add();
            DataGridViewRow row = dataGridView.Rows[rowIndex];

            row.Cells[0].Value = reelItem.Filename;
            row.Cells[1].Value = reelItem.StartCommand;
            row.Cells[2].Value = reelItem.StopCommand;
            row.Cells[3].Value = reelItem.MaxLengthSecs;
            row.Tag = reelItem;
        }

        private void BtnRotAdd_Click(object sender, EventArgs e)
        {
            if (cboRotNetCues.SelectedItem == null)
            {
                MessageBox.Show("Please select a NetCue before adding rotations.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NetCueItem selectedItem = (NetCueItem)cboRotNetCues.SelectedItem;
            int selectedId = selectedItem.Id;

            // Create a new rotation item
            RotationItem newItem = new RotationItem
            {
                NetCueID = selectedId,
                SortOrder = dgRots.Rows.Count + 1, // You need to set the appropriate sort order
                Marker = 0 // Set the default marker value here if needed
            };

            // Add a new row to the DataGridView
            int rowIndex = dgRots.Rows.Add();
            DataGridViewRow row = dgRots.Rows[rowIndex];
            row.Cells[0].Value = ""; // Set the marker cell value
            row.Cells[1].Value = ""; // Set the cart name cell value
            row.Cells[2].Value = ""; // Set the start date cell value
            row.Cells[3].Value = ""; // Set the end date cell value
            row.Cells[4].Value = ""; // Set the cart path cell value
            row.Tag = newItem; // Set the rotation item as the tag for the row

            // Set focus on the cell in column #1 (cart name cell) of the new row
            dgRots.CurrentCell = row.Cells[1]; // Change to the appropriate column index
            dgRots.BeginEdit(true); // Start editing the cell

            // Show the file dialog box and update the DataGridView cell
            SelectFileForCell(row.Cells[1], newItem);
        }

        private void SelectFileForCell(DataGridViewCell cell, RotationItem rotationItem)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select Cart File";
                openFileDialog.Filter = "All Supported Files|*.wav;*.mp3;*.rda|Wave Files (*.wav)|*.wav|MP3 Files (*.mp3)|*.mp3|RDS Phantom Audio (*.rda)|*.rda";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    string cartName = Path.GetFileName(selectedFilePath);

                    // Update the cell value
                    cell.Value = cartName;

                    // Get the index of the current row
                    int rowIndex = cell.RowIndex;

                    // Get the corresponding cell in column #4 (full path column)
                    DataGridViewCell fullPathCell = dgRots.Rows[rowIndex].Cells[4];

                    // Set the full path in the full path cell
                    fullPathCell.Value = selectedFilePath;

                    // Update the rotationItem object with the new values
                    rotationItem.Path = selectedFilePath;
                    rotationItem.CartName = cartName;

                    // Set the rotationItem as the tag for the row
                    dgRots.Rows[rowIndex].Tag = rotationItem;

                    // Save the changes to the database
                    SaveRotationItem(rotationItem);
                }
            }
        }

        private void DgRots_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                if(maskedTextBox.Visible)
                {

                    if (maskedTextBox.Text.Length == maskedTextBox.Mask.Length)
                    {
                        if (string.IsNullOrEmpty(maskedTextBox.Text))
                        {
                            dgRots.CurrentCell.Value = "";
                        }
                        else
                        {
                            string format = "yyyy-MM-dd HH:mm"; // Adjust the format as per your mask
                            if (DateTime.TryParseExact(maskedTextBox.Text, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                            {
                                dgRots.CurrentCell.Value = maskedTextBox.Text;
                            }
                            else
                            {
                                dgRots.CurrentCell.Value = "";
                            }
                        }
                    } else
                    {
                        dgRots.CurrentCell.Value = "";
                    }
                    maskedTextBox.Visible = false;
                }
                DataGridViewRow editedRow = dgRots.Rows[e.RowIndex];
                RotationItem rotationItem = (RotationItem)editedRow.Tag; // Retrieve the RotationItem from the row's Tag

                // Update the rotationItem object with the new values from the edited row
                rotationItem.StartDate = editedRow.Cells[2].Value?.ToString();
                rotationItem.EndDate = editedRow.Cells[3].Value?.ToString();
                NetCueItem selectedItem = (NetCueItem)cboRotNetCues.SelectedItem;
                int selectedId = selectedItem.Id;
                rotationItem.NetCueID = selectedId;

                SaveRotationItem(rotationItem); // Save the changes to the database
            }
        }

        private void SaveRotationItem(RotationItem item)
        {
            Cursor.Current = Cursors.WaitCursor;
            using (SQLiteConnection connection = new SQLiteConnection(DatabaseOperations.connectionString))
            {
                connection.Open();
                if (item.Id == 0) // New item
                {

                    string insertQuery = "INSERT INTO Rotations (NetCueID, SortOrder, Marker, CartPath, StartDate, EndDate) " +
                                         "VALUES (@NetCueID, @SortOrder, @Marker, @CartPath, @StartDate, @EndDate)";
                    using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@NetCueID", item.NetCueID);
                        insertCommand.Parameters.AddWithValue("@SortOrder", item.SortOrder);
                        insertCommand.Parameters.AddWithValue("@Marker", item.Marker);
                        insertCommand.Parameters.AddWithValue("@CartPath", item.Path);
                        insertCommand.Parameters.AddWithValue("@StartDate", item.StartDate);
                        insertCommand.Parameters.AddWithValue("@EndDate", item.EndDate);

                        insertCommand.ExecuteNonQuery();
                    }
                    // Retrieve the last inserted row's id
                    string getLastInsertedIdQuery = "SELECT last_insert_rowid()";
                    using (SQLiteCommand getLastInsertedIdCommand = new SQLiteCommand(getLastInsertedIdQuery, connection))
                    {
                        int lastInsertedId = Convert.ToInt32(getLastInsertedIdCommand.ExecuteScalar());
                        item.Id = lastInsertedId; // Update the item's Id
                    }
                }
                else // Existing item
                {
                    string updateQuery = "UPDATE Rotations SET NetCueID = @NetCueID, SortOrder = @SortOrder, " +
                                         "Marker = @Marker, CartPath = @CartPath, StartDate = @StartDate, EndDate = @EndDate " +
                                         "WHERE Id = @Id";
                    using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@NetCueID", item.NetCueID);
                        updateCommand.Parameters.AddWithValue("@SortOrder", item.SortOrder);
                        updateCommand.Parameters.AddWithValue("@Marker", item.Marker);
                        updateCommand.Parameters.AddWithValue("@CartPath", item.Path);
                        updateCommand.Parameters.AddWithValue("@StartDate", item.StartDate);
                        updateCommand.Parameters.AddWithValue("@EndDate", item.EndDate);
                        updateCommand.Parameters.AddWithValue("@Id", item.Id);
                        updateCommand.ExecuteNonQuery();
                    }
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void DgRots_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1) // Column index 1 corresponds to the cart name column
            {
                DataGridViewCell cartNameCell = dgRots.Rows[e.RowIndex].Cells[e.ColumnIndex];
                RotationItem rotationItem = (RotationItem)dgRots.Rows[e.RowIndex].Tag;
                SelectFileForCell(cartNameCell, rotationItem);
            }
            // Check if any cell within a row is clicked
            bool isCellClicked = e.RowIndex >= 0 && e.ColumnIndex >= 0;
            // Enable or disable the "Delete" button based on the cell click status
            btnRotDelete.Enabled = isCellClicked;
        }

        private void BtnRotDelete_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DataGridViewCell selectedCell = dgRots.SelectedCells[0]; // Get the selected cell
            int rowIndex = selectedCell.RowIndex; // Get the row index of the selected cell

            if (rowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgRots.Rows[rowIndex];
                if (selectedRow.Tag is RotationItem rotationItem)
                {
                    using (SQLiteConnection connection = new SQLiteConnection(DatabaseOperations.connectionString))
                    {
                        connection.Open();

                        string deleteQuery = "DELETE FROM Rotations WHERE Id = @Id";
                        using (SQLiteCommand deleteCommand = new SQLiteCommand(deleteQuery, connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@Id", rotationItem.Id);
                            deleteCommand.ExecuteNonQuery();
                        }
                    }

                    dgRots.Rows.RemoveAt(rowIndex); // Remove the selected row
                    btnRotDelete.Enabled = false;
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void DgRots_SelectionChanged(object sender, EventArgs e)
        {
            // Check if any row is selected in the DataGridView
            bool isRowSelected = dgRots.SelectedRows.Count > 0;

            // Enable or disable the "Delete" button based on the selection status
            btnRotDelete.Enabled = isRowSelected;
        }

        private void DgRots_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt) e.SuppressKeyPress = true;
            // Check if the Alt key is pressed and the D key is pressed simultaneously
            if (e.Alt && e.KeyCode == Keys.D)
            {
                // Call the existing delete button's click event handler
                BtnRotDelete_Click(sender, e);

                // Prevent further processing of the key event
                e.Handled = true;
            }
            else if (e.Alt && e.KeyCode == Keys.M)
            {
                // Prevent further processing of the key event
                e.Handled = true;
                DataGridViewCell selectedCell = dgRots.SelectedCells[0]; // Get the selected cell
                int rowIndex = selectedCell.RowIndex; // Get the row index of the selected cell

                if (rowIndex >= 0)
                {
                    DataGridViewRow selectedRow = dgRots.Rows[rowIndex];
                    if (selectedRow.Tag is RotationItem rotationItem)
                    {
                        using (SQLiteConnection connection = new SQLiteConnection(DatabaseOperations.connectionString))
                        {
                            connection.Open();

                            NetCueItem selectedItem = (NetCueItem)cboRotNetCues.SelectedItem;
                            int netCueID = selectedItem.Id;

                            string resetMarkersQuery = "UPDATE Rotations SET Marker = 0 WHERE NetCueID = @NetCueID";
                            using (SQLiteCommand resetMarkersCommand = new SQLiteCommand(resetMarkersQuery, connection))
                            {
                                resetMarkersCommand.Parameters.AddWithValue("@NetCueID", netCueID);
                                resetMarkersCommand.ExecuteNonQuery();
                            }

                            // Set the new marker value for the specified NetCueID
                            string updateMarkerQuery = "UPDATE Rotations SET Marker = @Marker WHERE NetCueID = @NetCueID AND Id = @Id";
                            using (SQLiteCommand updateMarkerCommand = new SQLiteCommand(updateMarkerQuery, connection))
                            {
                                updateMarkerCommand.Parameters.AddWithValue("@NetCueID", netCueID);
                                updateMarkerCommand.Parameters.AddWithValue("@Id", rotationItem.Id);
                                updateMarkerCommand.Parameters.AddWithValue("@Marker", 1);
                                updateMarkerCommand.ExecuteNonQuery();
                            }

                            // Update the ">" marker in the DataGridView
                            UpdateMarkerInDataGridView(rotationItem.Id);
                        }
                        btnRotDelete.Enabled = false;
                    }
                }

            }
            else if (e.Alt && e.KeyCode == Keys.A)
            {
                // Call the existing delete button's click event handler
                BtnRotAdd_Click(sender, e);

                // Prevent further processing of the key event
                e.Handled = true;
            }
            else if (e.Alt && e.KeyCode == Keys.Up)
            {
                e.Handled = true;
                MoveSelectedRow(-1); // Move the selected row up
            }
            else if (e.Alt && e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                MoveSelectedRow(1); // Move the selected row down
            }
        }

        private void MoveSelectedRow(int offset)
        {
            if (dgRots.SelectedCells.Count == 0) return;

            DataGridViewCell selectedCell = dgRots.SelectedCells[0];
            int rowIndex = selectedCell.RowIndex;

            // Calculate the new row index after moving
            int newRowindex = rowIndex + offset;

            if (newRowindex < 0 || newRowindex >= dgRots.Rows.Count) return;

            // Swap the rows
            DataGridViewRow selectedRow = dgRots.Rows[rowIndex];
            dgRots.Rows.RemoveAt(rowIndex);
            dgRots.Rows.Insert(newRowindex, selectedRow);

            // Update SortOrder values based on new order
            UpdateSortOrderValuesInDatabase();

            // Update the selected cell after moving the row
            UpdateSelectedCellAfterMove(newRowindex);
        }


        private void UpdateSortOrderValuesInDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DatabaseOperations.connectionString))
            {
                connection.Open();

                for (int i = 0; i < dgRots.Rows.Count; i++)
                {
                    DataGridViewRow row = dgRots.Rows[i];
                    RotationItem rotationItem = (RotationItem)row.Tag;

                    string updateQuery = "UPDATE Rotations SET SortOrder = @SortOrder WHERE Id = @Id";
                    using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@SortOrder", i + 1);
                        updateCommand.Parameters.AddWithValue("@Id", rotationItem.Id);
                        updateCommand.ExecuteNonQuery();
                    }
                }
            }
        }
        private void UpdateSelectedCellAfterMove(int newIndex)
        {
            if (dgRots.SelectedCells.Count == 0) return;

            int selectedColumnIndex = dgRots.SelectedCells[0].ColumnIndex;
            dgRots.ClearSelection();
            dgRots.Rows[newIndex].Cells[selectedColumnIndex].Selected = true;
        }
        private void UpdateMarkerInDataGridView(int markerID)
        {
            foreach (DataGridViewRow row in dgRots.Rows)
            {
                RotationItem rotationItem = (RotationItem)row.Tag;
                if (rotationItem.Id == markerID)
                {
                    row.Cells[0].Value = ">";
                }
                else
                {
                    row.Cells[0].Value = "";
                }
            }
        }

        private void DgRots_Scroll(object sender, ScrollEventArgs e)
        {
            if(maskedTextBox.Visible)
            {
                Rectangle rect = dgRots.GetCellDisplayRectangle(dgRots.CurrentCell.ColumnIndex, dgRots.CurrentCell.RowIndex, true);
                maskedTextBox.Location = rect.Location;

            }
        }

        private void CheckMuteNetCuePlayback_CheckedChanged(object sender, EventArgs e)
        {
            // Unsubscribe from the event temporarily
            CheckMuteNetCuePlayback.CheckedChanged -= CheckMuteNetCuePlayback_CheckedChanged;

            using (SQLiteConnection connection = new SQLiteConnection(DatabaseOperations.connectionString))
            {
                connection.Open();
                if (sender is CheckBox checkBox)
                {
                    NetCueItem selectedItem = (NetCueItem)cboRotNetCues.SelectedItem;
                    int selectedId = selectedItem.Id;

                    string updateQuery = "UPDATE NetCues SET Muted = @Muted WHERE Id = @Id";
                    using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@Muted", checkBox.Checked ? 1 : 0);
                        updateCommand.Parameters.AddWithValue("@Id", selectedId);
                        updateCommand.ExecuteNonQuery();
                    }
                    // Change the text color based on the checkbox state
                    checkBox.ForeColor = checkBox.Checked ? Color.Green : Color.Red;
                }
            }

            // Subscribe back to the event
            CheckMuteNetCuePlayback.CheckedChanged += CheckMuteNetCuePlayback_CheckedChanged;
        }

        private void CboAudioFeedInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAudioFeedInput.SelectedItem is AudioDeviceInfo selectedDevice)
            {
                // Save the selected audio input device ID to the Settings table
                SaveSelectedAudioInputDevice(selectedDevice.FriendlyName);
            }
        }
        private void SaveSelectedAudioInputDevice(string deviceId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(DatabaseOperations.connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE Settings SET AudioFeedDevice = @DeviceId WHERE Id = @Id";
                using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@DeviceId", deviceId);
                    updateCommand.Parameters.AddWithValue("@Id", 1); // Assuming 'Id' value for the settings row

                    updateCommand.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        private void CboAudioOutput_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Save the selected audio input device ID to the Settings table
            SaveSelectedAudioOutputDevice(cboAudioOutput.SelectedItem.ToString());
        }
        private void SaveSelectedAudioOutputDevice(string deviceId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(DatabaseOperations.connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE Settings SET AudioOutputDevice = @DeviceId WHERE Id = @Id";
                using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@DeviceId", deviceId);
                    updateCommand.Parameters.AddWithValue("@Id", 1); // Assuming 'Id' value for the settings row

                    updateCommand.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if the selected tab is the one containing the audio input devices
            if (tabControl1.SelectedTab == tabHardware)
            {
                ReloadAndEnumerateAudioDevices();
            } else if(tabControl1.SelectedTab == tabNetCues)
            {
                ResetNetCueForm(true);
                LoadNetCuesList();
                cboRotType.SelectedIndex = 0;
                dgRots.Rows.Clear();
                var maskedTextBox = new MaskedTextBox
                {
                    Visible = false
                };
                dgRots.Controls.Add(maskedTextBox);
            } else if(tabControl1.SelectedTab == tabGeneral)
            {
                LoadGeneralSettingsScreen();
            } else if(tabControl1.SelectedTab == tabReeltoReel)
            {
                LoadReelToReelScreen();
            }
        }

        public void LoadReelToReelScreen()
        {
            btnReel2ReelAdd.Enabled = false;
            btnReel2ReelDelete.Enabled = false;
            btnReel2ReelEdit.Enabled = false;
            dgReelToReel.Rows.Clear();
            Cursor.Current = Cursors.WaitCursor;
            using (SQLiteConnection connection = new SQLiteConnection(DatabaseOperations.connectionString))
            {
                connection.Open();
                string selectQuery = "SELECT Id, Format, Filename, StartCommand, StopCommand, MaxLengthSecs, FTPServerId, FTPPath FROM ReelToReel ORDER BY Id DESC";
                using (SQLiteCommand reelSelectCommand = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataReader reader = reelSelectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            int format = reader.GetInt32(1);
                            string filename = reader.GetString(2);
                            string startcommand = reader.GetString(3);
                            string stopcommand = reader.GetString(4);
                            int maxlengthsecs = reader.GetInt32(5);
                            int ftpServerId = reader.GetInt32(6);
                            string ftpPath = reader.GetString(7);

                            ReelItem reelItem = new ReelItem
                            {
                                Id = id,
                                Format = format,
                                Filename = filename,
                                StartCommand = startcommand,
                                StopCommand = stopcommand,
                                MaxLengthSecs = maxlengthsecs,
                                FTPServerId = ftpServerId,
                                FTPPath = ftpPath
                            };

                            PopulateReelToReelDataGridView(dgReelToReel, reelItem);
                        }
                    }
                }
            }
            btnReel2ReelAdd.Enabled = true;
            if (dgReelToReel.SelectedRows.Count == 1)
            {
                btnReel2ReelDelete.Enabled = true;
                btnReel2ReelEdit.Enabled = true;
            }
            Cursor.Current = Cursors.Default;
        }


        private void LoadGeneralSettingsScreen()
        {
            Cursor.Current = Cursors.WaitCursor;
            
            // Set Volume Slider Defaults
            sliderInputVolume.Minimum = 0;
            sliderInputVolume.Maximum = 100;

            GeneralSettings settings = DatabaseOperations.GetGeneralSettingsFromDatabase();
            if (settings != null)
            {
                TextBoxUDPListener.Text = settings.NetCuePort.ToString();

                int selectedIndex = ComboBoxNetCueProcessingMode.FindString(settings.NetCueProcessingMode);

                if (selectedIndex != -1)
                {
                    ComboBoxNetCueProcessingMode.SelectedIndex = selectedIndex;
                }
                TextBoxStartNetCue.Text = settings.NetCueStartCommand.ToString();
                TextBoxStopNetCue.Text = settings.NetCueStopCommand.ToString();

                // Set Volume Slider
                int volume = (int)(float.Parse(settings.AudioFeedVolume) * sliderInputVolume.Maximum);
                sliderInputVolume.Value = volume;

                if(settings.InputVolumeControl == 1)
                {
                    chkEnableVolumeControl.Checked = true;
                    label9.Enabled = true;
                    sliderInputVolume.Enabled = true;
                } else
                {
                    chkEnableVolumeControl.Checked = false;
                    label9.Enabled = false;
                    sliderInputVolume.Enabled = false;
                }

                if(ComboBoxNetCueProcessingMode.SelectedIndex == 0)
                {
                    TextBoxStartNetCue.Enabled = false;
                    TextBoxStopNetCue.Enabled = false;
                    LabelNetCueStart.Enabled = false;
                    LabelNetCueStop.Enabled = false;
                } else
                {
                    TextBoxStartNetCue.Enabled = true;
                    TextBoxStopNetCue.Enabled = true;
                    LabelNetCueStart.Enabled = true;
                    LabelNetCueStop.Enabled = true;
                }

            }
            else
            {
                // Handle the case where no matching record was found
                TextBoxUDPListener.Text = "9963";
                ComboBoxNetCueProcessingMode.SelectedIndex = 0;
                TextBoxStartNetCue.Text = "";
                TextBoxStopNetCue.Text = "";
            }
            Cursor.Current = Cursors.Default;
        }

        private void ReloadAndEnumerateAudioDevices()
        {
            Cursor.Current = Cursors.WaitCursor;
            EnumerateInputDevices();
            EnumerateOutputDevices();

            // Retrieve the selected audio input device ID from the database
            string selectedInputDeviceId = GetSelectedAudioInputDeviceIdFromDatabase();
            string selectedOutputDeviceId = GetSelectedAudioOutputDeviceIdFromDatabase();

            // Find and select the corresponding device in the combobox
            foreach (AudioDeviceInfo deviceInfo in cboAudioFeedInput.Items)
            {
                if (deviceInfo.DeviceId == selectedInputDeviceId)
                {
                    cboAudioFeedInput.SelectedItem = deviceInfo;
                    break;
                }
            }

            int selectedIndex = cboAudioOutput.FindString(selectedOutputDeviceId);
            if (selectedIndex != -1)
            {
                cboAudioOutput.SelectedIndex = selectedIndex;
            }


            Cursor.Current = Cursors.Default;
        }
        private string GetSelectedAudioInputDeviceIdFromDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DatabaseOperations.connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT AudioFeedDevice FROM Settings WHERE Id = @Id";
                using (SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@Id", 1); // Assuming 'Id' value for the settings row
                    object selectedDeviceId = selectCommand.ExecuteScalar();
                    return selectedDeviceId?.ToString();
                }
            }
        }
        private string GetSelectedAudioOutputDeviceIdFromDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DatabaseOperations.connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT AudioOutputDevice FROM Settings WHERE Id = @Id";
                using (SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, connection))
                {
                    selectCommand.Parameters.AddWithValue("@Id", 1); // Assuming 'Id' value for the settings row
                    object selectedDeviceId = selectCommand.ExecuteScalar();
                    return selectedDeviceId?.ToString();
                }
            }
        }

        private void ComboBoxNetCueProcessingMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnSaveGeneral.Enabled = true;
            BtnCancelGeneral.Enabled = true;

            if (ComboBoxNetCueProcessingMode.SelectedIndex == 0)
            {
                TextBoxStartNetCue.Enabled = false;
                TextBoxStopNetCue.Enabled = false;
                LabelNetCueStart.Enabled = false;
                LabelNetCueStop.Enabled = false;
            } else
            {
                TextBoxStartNetCue.Enabled = true;
                TextBoxStopNetCue.Enabled = true;
                LabelNetCueStart.Enabled = true;
                LabelNetCueStop.Enabled = true;
            }
        }

        private void BtnCancelGeneral_Click(object sender, EventArgs e)
        {
            LoadGeneralSettingsScreen();
            BtnSaveGeneral.Enabled = false;
            BtnCancelGeneral.Enabled = false;
        }

        private void BtnSaveGeneral_Click(object sender, EventArgs e)
        {
            BtnSaveGeneral.Enabled = false;
            BtnCancelGeneral.Enabled = false;

            GeneralSettings updatedSettings = new GeneralSettings
            {
                NetCuePort = int.Parse(TextBoxUDPListener.Text),
                NetCueProcessingMode = ComboBoxNetCueProcessingMode.SelectedItem.ToString(),
                NetCueStartCommand = TextBoxStartNetCue.Text,
                NetCueStopCommand = TextBoxStopNetCue.Text,
                AudioFeedVolume = ((float)sliderInputVolume.Value / sliderInputVolume.Maximum).ToString(),
                InputVolumeControl = chkEnableVolumeControl.Checked ? 1 : 0
            };
            UpdateGeneralSettings(updatedSettings);
            parentForm.UpdateMode("Mode: " + ComboBoxNetCueProcessingMode.SelectedItem.ToString());
        }

        private void UpdateGeneralSettings(GeneralSettings updatedSettings)
        {
            using (SQLiteConnection connection = new SQLiteConnection(DatabaseOperations.connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE Settings SET NetCuePort = @Port, NetCueProcessingMode = @Mode, NetCueStartCommand = @StartCommand, NetCueStopCommand = @StopCommand, AudioFeedVolume = @AudioFeedVolume, InputVolumeControl = @InputVolumeControl WHERE Id = @Id";

                using (SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@Id", 1); // Assuming 'Id' value for the settings row
                    updateCommand.Parameters.AddWithValue("@Port", updatedSettings.NetCuePort);
                    updateCommand.Parameters.AddWithValue("@Mode", updatedSettings.NetCueProcessingMode);
                    updateCommand.Parameters.AddWithValue("@StartCommand", updatedSettings.NetCueStartCommand);
                    updateCommand.Parameters.AddWithValue("@StopCommand", updatedSettings.NetCueStopCommand);
                    updateCommand.Parameters.AddWithValue("@AudioFeedVolume", updatedSettings.AudioFeedVolume);
                    updateCommand.Parameters.AddWithValue("@InputVolumeControl", updatedSettings.InputVolumeControl);

                    updateCommand.ExecuteNonQuery();
                }
            }
        }

        private void TextBoxUDPListener_TextChanged(object sender, EventArgs e)
        {
            BtnSaveGeneral.Enabled = true;
            BtnCancelGeneral.Enabled = true;
        }

        private void TextBoxStartNetCue_TextChanged(object sender, EventArgs e)
        {
            BtnSaveGeneral.Enabled = true;
            BtnCancelGeneral.Enabled = true;
        }

        private void TextBoxStopNetCue_TextChanged(object sender, EventArgs e)
        {
            BtnSaveGeneral.Enabled = true;
            BtnCancelGeneral.Enabled = true;
        }

        private void SliderInputVolume_Scroll(object sender, EventArgs e)
        {
            BtnCancelGeneral.Enabled = true;
            BtnSaveGeneral.Enabled = true;

            float linearVolume = (float)sliderInputVolume.Value / sliderInputVolume.Maximum;
            float dB = ConvertLinearToDB(linearVolume);
            ttVolume.SetToolTip(sliderInputVolume, $"Volume: {dB:F2} dB");
        }

        private void BtnReel2ReelAdd_Click(object sender, EventArgs e)
        {
            FormReelToReel FormReelToReel = new FormReelToReel(this, "add");
            FormReelToReel.ShowDialog();
        }

        private void BtnReel2ReelEdit_Click(object sender, EventArgs e)
        {
            if (dgReelToReel.SelectedRows.Count == 1)
            {
                int selectedRowIndex = dgReelToReel.SelectedRows[0].Index;
                // Load the reelItem object into a variable for use in the edit screen.
                ReelItem editReelItem = (ReelItem)dgReelToReel.Rows[selectedRowIndex].Tag;
                FormReelToReel FormReelToReel = new FormReelToReel(this, "edit", editReelItem);
                FormReelToReel.ShowDialog();
            }
            else
            {
                btnReel2ReelEdit.Enabled = false;
                btnReel2ReelDelete.Enabled = false;
            }
        }

        private void BtnFTPSetup_Click(object sender, EventArgs e)
        {
            FormFTPSetup FormFTPSetup = new FormFTPSetup();
            FormFTPSetup.ShowDialog();
        }

        private void chkEnableVolumeControl_CheckedChanged(object sender, EventArgs e)
        {
            if(!chkEnableVolumeControl.Checked)
            {
                label9.Enabled = false;
                sliderInputVolume.Enabled = false;
            } else
            {
                label9.Enabled = true;
                sliderInputVolume.Enabled = true;
            }
            BtnCancelGeneral.Enabled = true;
            BtnSaveGeneral.Enabled = true;
        }
    }
}
