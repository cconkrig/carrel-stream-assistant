using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carrel_Stream_Assistant
{
    class AudioDeviceInfo
    {
        public string DeviceId { get; }
        public string FriendlyName { get; }

        public AudioDeviceInfo(string deviceId, string friendlyName)
        {
            DeviceId = deviceId;
            FriendlyName = friendlyName;
        }

        public override string ToString()
        {
            return FriendlyName; // Display the friendly name in the ComboBox
        }
    }
}
