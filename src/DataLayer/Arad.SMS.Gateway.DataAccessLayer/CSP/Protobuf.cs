using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CPS
{
    /// <summary>
    /// System Stream wrapper for reading and writing Google Protobuf encoded data.
    /// </summary>
    public class Protobuf_Streamer
    {
        /// <summary>
        /// Creates new Protobuf_Stream class instance.
        /// </summary>
        /// <param name="strm">Stream to use for reading and writing.</param>
        public Protobuf_Streamer(Stream strm)
        {
            this.p_strm = strm;
        }

        /// <summary>
        /// Reads one Varint from stream.
        /// </summary>
        /// <returns>Decoded Varint.</returns>
        public Int64 ReadVarint()
        {
            Int64 ret = 0;
            Int64 quot = 1;

            while (this.p_strm.Position < this.p_strm.Length)
            {
                byte b = (byte)this.p_strm.ReadByte();
                ret += quot * (b & 0x7F);
                quot *= 128;
                if ((b & 0x80) == 0)
                    break;
            }

            return ret;
        }

        /// <summary>
        /// Reads fixed number of bytes from stream.
        /// </summary>
        /// <param name="size">Number of bytes to read.</param>
        /// <returns>Byte array of read data.</returns>
        public byte[] ReadBytes(int size)
        {
            byte[] ret = new byte[size];
            int len = this.p_strm.Read(ret, 0, size);

            if (len < size)
            {
                byte[] ret2 = new byte[len];
                Array.Copy(ret, ret2, len);
                ret = ret2;
            }

            return ret;
        }

        /// <summary>
        /// Writes one Varint to stream.
        /// </summary>
        /// <param name="value">Varint to write.</param>
        /// <returns>Number of bytes written.</returns>
        public int WriteVarint(Int64 value)
        {
            if (value == 0)
            {
                this.p_strm.WriteByte(0);
                return 1;
            }

            int ret = 0;

            while (value > 0)
            {
                byte b = (byte)(value % 128);
                value = value / 128;
                if (value > 0)
                    b |= 0x80;
                this.p_strm.WriteByte(b);
                ret += 1;
            }

            return ret;
        }

        /// <summary>
        /// Writes fixed number of bytes to stream.
        /// </summary>
        /// <param name="data">Byte array to write.</param>
        /// <param name="size">Number of bytes to write.</param>
        /// <param name="offset">Start offset in byte array.</param>
        /// <returns>Number of bytes written.</returns>
        public int WriteBytes(byte[] data, int size, int offset = 0)
        {
            this.p_strm.Write(data, offset, size);
            return size;
        }

        /// <summary>
        /// Gets wrapped stream.
        /// </summary>
        /// <returns>Stream.</returns>
        public Stream GetStream()
        {
            return this.p_strm;
        }

        /// <summary>
        /// Used stream.
        /// </summary>
        private Stream p_strm;
    }

    /// <summary>
    /// Google Protobuf field class. Used internally and not available outside library.
    /// </summary>
    class Protobuf_Field
    {
        /// <summary>
        /// Creates new Protobuf_Field class instance from passed parameters.
        /// </summary>
        /// <param name="fn">Field number.</param>
        /// <param name="wt">Wire type.</param>
        /// <param name="data">Raw data.</param>
        public Protobuf_Field(ushort fn, Protobuf.WireType wt, byte[] data)
        {
            this.p_fn = fn;
            this.p_wt = wt;
            this.p_data = data;
        }

        /// <summary>
        /// Creates new Protobuf_Field class instance by reading and decoding it from Protobuf_Streamer.
        /// </summary>
        /// <param name="streamer">Protobuf_Streamer for reading.</param>
        public Protobuf_Field(Protobuf_Streamer streamer)
        {
            Int64 head = streamer.ReadVarint();
            if (head == 0) // end of stream or read error
                throw new Exception("End of underlaying stream of read error");

            this.p_wt = (Protobuf.WireType)(head & 0x07);
            this.p_fn = (ushort)(head >> 3);
            this.p_data = null;
            this.p_idata = 0;

            switch (this.p_wt)
            {
                case Protobuf.WireType.Varint:
                    this.p_idata = streamer.ReadVarint();
                    break;
                case Protobuf.WireType.Fixed64bit:
                    this.p_data = streamer.ReadBytes(8);
                    break;
                case Protobuf.WireType.LengthDelimited:
                    int len = (int)(streamer.ReadVarint());
                    this.p_data = streamer.ReadBytes(len);
                    break;
                /*case Protobuf.WireType_StartGroup: -- deprecated
                    break;
                case Protobuf.WireType_EndGroup: -- deprecated
                    break;*/
                case Protobuf.WireType.Fixed32bit:
                    this.p_data = streamer.ReadBytes(4);
                    break;
                default:
                    throw new Exception("Unknown protobuf wire type " + this.p_wt);
            }
        }

        /// <summary>
        /// Gets field's write type.
        /// </summary>
        /// <returns>Write type.</returns>
        public Protobuf.WireType GetWireType()
        {
            return this.p_wt;
        }

        /// <summary>
        /// Gets field's field number.
        /// </summary>
        /// <returns>Field number.</returns>
        public ushort GetFieldNumber()
        {
            return this.p_fn;
        }

        /// <summary>
        /// Gets field's raw data.
        /// </summary>
        /// <returns>Raw data.</returns>
        public byte[] GetData()
        {
            return this.p_data;
        }

        /// <summary>
        /// Gets field's data, if value is Varint.
        /// </summary>
        /// <returns>Varint.</returns>
        public Int64 GetIData()
        {
            return this.p_idata;
        }

        /// <summary>
        /// Writes field to Protobuf_Streamer.
        /// </summary>
        /// <param name="streamer">Protobuf_Streamer for writing field to.</param>
        public void WriteToStream(Protobuf_Streamer streamer)
        {
            Int64 head = (this.p_fn << 3) | (ushort)(this.p_wt);
            streamer.WriteVarint(head);
            if (this.p_wt == Protobuf.WireType.LengthDelimited)
                streamer.WriteVarint(this.p_data.Length);
            streamer.WriteBytes(this.p_data, this.p_data.Length);
        }

        /// <summary>
        /// Field number.
        /// </summary>
        private ushort p_fn;
        /// <summary>
        /// Write type.
        /// </summary>
        private Protobuf.WireType p_wt;
        /// <summary>
        /// Raw data.
        /// </summary>
        private byte[] p_data;
        /// <summary>
        /// Varint data.
        /// </summary>
        private Int64 p_idata;

    }
    
    /// <summary>
    /// Very simple Google Protobuf API class. For CPS library we do not need complete Protobuf package, this simple implementation is enough.
    /// </summary>
    public class Protobuf
    {
        /// <summary>
        /// Google Protobuf field's wire type.
        /// </summary>
        public enum WireType
        {
            /// <summary>
            /// Varint (variable size integer).
            /// </summary>
            Varint = 0,
            /// <summary>
            /// Fixed 64bit.
            /// </summary>
            Fixed64bit = 1,
            /// <summary>
            /// Length-delimited field, for example string.
            /// </summary>
            LengthDelimited = 2,
            /// <summary>
            /// Start Group marker - deprecated.
            /// </summary>
            StartGroup = 3,
            /// <summary>
            /// End Group marker - deprecated.
            /// </summary>
            EndGroup = 4,
            /// <summary>
            /// Fixed 32bit.
            /// </summary>
            Fixed32bit = 5
        };

        /// <summary>
        /// Creates new Protobuf class instance with no fields.
        /// </summary>
        public Protobuf()
        {
            p_fields = new Dictionary<ushort, Protobuf_Field>();
        }

        /// <summary>
        /// Creates new Protobuf class instance by reading it from Protobuf_Streamer.
        /// </summary>
        /// <param name="streamer">Protobuf_Streamer used for reading.</param>
        public Protobuf(Protobuf_Streamer streamer)
        {
            p_fields = new Dictionary<ushort, Protobuf_Field>();

            Protobuf_Field field = null;

            while (true)
            {
                try
                {
                    field = new Protobuf_Field(streamer);
                }
                catch (Exception)
                {
                    break;
                }

                this.p_fields[field.GetFieldNumber()] = field;
            }
        }

        /// <summary>
        /// Creates new Protobuf field from given data.
        /// </summary>
        /// <param name="fn">Field number.</param>
        /// <param name="wt">Wire type.</param>
        /// <param name="data">Raw data.</param>
        public void CreateField(ushort fn, WireType wt, byte[] data)
        {
            this.p_fields[fn] = new Protobuf_Field(fn, wt, data);
        }

        /// <summary>
        /// Creates new Protobuf string field from given data.
        /// </summary>
        /// <param name="fn">Field number.</param>
        /// <param name="data">String data.</param>
        public void CreateStringField(ushort fn, string data)
        {
            byte[] buf = Encoding.UTF8.GetBytes(data);
            this.CreateField(fn, WireType.LengthDelimited, buf);
        }

        /// <summary>
        /// Creates new Protobuf fixed64 field from given data.
        /// </summary>
        /// <param name="fn">Field number.</param>
        /// <param name="value">64-bit integer.</param>
        public void CreateFixed64Field(ushort fn, UInt64 value)
        {
            byte[] buf = BitConverter.GetBytes(value);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(buf);
            this.CreateField(fn, WireType.Fixed64bit, buf);
        }

        /// <summary>
        /// Gets field's wire type by field number.
        /// </summary>
        /// <param name="fn">Field number.</param>
        /// <returns>Field's wire type.</returns>
        public WireType GetFieldWireType(ushort fn)
        {
            try
            {
                return this.p_fields[fn].GetWireType();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets field's raw data by field number.
        /// </summary>
        /// <param name="fn">Field number.</param>
        /// <returns>Byte array.</returns>
        public byte[] GetField(ushort fn)
        {
            try
            {
                return this.p_fields[fn].GetData();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets field's data as string by field number.
        /// </summary>
        /// <param name="fn">Field number.</param>
        /// <returns>String data.</returns>
        public string GetStringField(ushort fn)
        {
            try
            {
                byte[] buf = this.p_fields[fn].GetData();
                return Encoding.UTF8.GetString(buf);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Writes whole Protobuf to Protobuf_Streamer.
        /// </summary>
        /// <param name="streamer">Protobuf_Streamer for writing.</param>
        public void WriteToStream(Protobuf_Streamer streamer)
        {
            foreach (KeyValuePair<ushort, Protobuf_Field> pair in this.p_fields)
                pair.Value.WriteToStream(streamer);
        }

        /// <summary>
        /// Protobuf fields.
        /// </summary>
        private Dictionary<ushort, Protobuf_Field> p_fields;
    }
}
