using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using FreePIE.Core.Contracts;


namespace VireioSMTracker
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VireioSMTData
    {
        public int DataID;                  // increases everytime data is writen

        public float Yaw;                   // yaw,pitch,roll are in degrees
        public float Pitch;
        public float Roll;

        public float X;
        public float Y;
        public float Z;
    };

    [GlobalType(Type = typeof(VireioSMTGlobal))]
    public class VireioSMT : IPlugin
    {
        public bool newData;

        private MemoryMappedFile memoryMappedFile;
        private MemoryMappedViewAccessor accessor;

        public VireioSMTData data { get; set; }

        public object CreateGlobal()
        {
            return new VireioSMTGlobal(this);
        }

        public Action Start()
        {
            memoryMappedFile = MemoryMappedFile.CreateOrOpen("VireioSMTrack", Marshal.SizeOf(typeof(VireioSMTData)));
            accessor = memoryMappedFile.CreateViewAccessor();
            return null;
        }

        public void Stop()
        {
            accessor.Dispose();
            memoryMappedFile.Dispose();
        }

        public event EventHandler Started;

        public string FriendlyName
        {
            get { return "Vireio Perception Shared Memory Tracker"; }
        }

        public bool GetProperty(int index, IPluginProperty property)
        {
            return false;
        }

        public bool SetProperties(Dictionary<string, object> properties)
        {
            return false;
        }

        public void DoBeforeNextExecute()
        {
            if (newData)
            {
                Send();
                newData = false;
            }
        }

        private void Send()
        {
            var local = data;
            local.DataID++;
            data = local;
            accessor.Write(0, ref local);
        }
    }

    [Global(Name = "vireioSMT")]
    public class VireioSMTGlobal
    {
        private readonly VireioSMT vireioSMT;

        public VireioSMTGlobal(VireioSMT VireioSMT)
        {
            this.vireioSMT = VireioSMT;
        }

        private void Write(Func<VireioSMTData, VireioSMTData> setValue)
        {
            vireioSMT.newData = true;
            Console.WriteLine(vireioSMT.data.Roll);
            var data = vireioSMT.data;
            vireioSMT.data = setValue(data);
        }

        public float yaw
        {
            get { return vireioSMT.data.Yaw; }
            set { Write(d => { d.Yaw = value; return d; }); }
        }

        public float pitch
        {
            get { return vireioSMT.data.Pitch; }
            set { Write(d => { d.Pitch = value; return d; }); }
        }

        public float roll
        {
            get { return vireioSMT.data.Roll; }
            set { Write(d => { d.Roll = value; return d; }); }
        }

        public float x
        {
            get { return vireioSMT.data.X; }
            set { Write(d => { d.X = value; return d; }); }
        }

        public float y
        {
            get { return vireioSMT.data.Y; }
            set { Write(d => { d.Y = value; return d; }); }
        }

        public float z
        {
            get { return vireioSMT.data.Z; }
            set { Write(d => { d.Z = value; return d; }); }
        }
    }
}