﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Danmaku.Model
{
    [SerializableAttribute]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot("i", Namespace = "", IsNullable = false)]
    public class BilibiliDanmakuData
    {
        public BilibiliDanmakuData() { }

        public BilibiliDanmakuData(List<DanmakuData> ds)
        {
            D = ds.Select(d => new iD
            {
                P = $"{d.Time},{(d.Type == 2 ? 4 : d.Type == 1 ? 5 : 1)},25,{d.Color},1512931469,1,354b5ade,4028451968",
                Value = d.Text
            }).ToArray();
        }

        [XmlElement("d")] public iD[] D { get; set; }

        /// <summary>
        ///     转通用Data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DanmakuData> ToDanmakuDataList()
        {
            return D.Select(s =>
            {
                var d = s.P.Split(",");
                var t = int.Parse(d[1]);
                return new DanmakuData
                {
                    Time = float.Parse(d[0]),
                    Color = int.Parse(d[3]),
                    Type = t == 4 ? 2 : t == 5 ? 1 : 0,
                    Author = "",
                    Text = s.Value
                };
            });
        }

        public string ToXml()
        {
            using var ms = new MemoryStream();
            var x = new XmlSerializer(typeof(BilibiliDanmakuData));
            x.Serialize(ms, this);
            return Encoding.UTF8.GetString(ms.ToArray());
        }
    }

    [SerializableAttribute]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    // ReSharper disable once InconsistentNaming
    public class iD
    {
        [XmlAttribute("p")] public string P { get; set; }

        [XmlText] public string Value { get; set; }
    }
}