using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpticalFiber
{
    class DataInit
    {
        public DataInit()
        {

            for (int i = 0; i < 9; i++)
            {
                DataClass.list_DeviceStatus.Add(new Class_DeviceStatus());
                DataClass.list_DeviceParam.Add(new Class_DeviceParam());

                DataClass.list_DeviceTemper.Add(new Class_DevcieTemper());

                DataClass.list_DeviceChannelParam.Add(new Class_DeviceChannelParam());
                DataClass.list_DevicePartition.Add(new Class_DevicePartition());

            }
          
        }
    }
}
