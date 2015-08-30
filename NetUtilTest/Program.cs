﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMapChinaRegion;
using NetUtil;
using GMap.NET;
using GMap.NET.MapProviders;
using GMapProvidersExt;
using GMapProvidersExt.Tencent;

namespace NetUtilTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Country china = GetCountryDataFromFile(@"F:\GMap\china-province-city-piecearea.xml");
            for (int i = 0; i < china.Province.Count; ++i)
            {
                string ps = china.Province[i].rings;
                china.Province[i].rings = GetPositionFromRings(ref ps, china.Province[i].name);
                for (int j = 0; j < china.Province[i].City.Count; ++j)
                {
                    string cs = china.Province[i].City[j].rings;
                    china.Province[i].City[j].rings = GetPositionFromRings(ref cs, china.Province[i].City[j].name);
                    for (int k = 0; k < china.Province[i].City[j].Piecearea.Count; ++k)
                    {
                        string pps = china.Province[i].City[j].Piecearea[k].rings;
                        china.Province[i].City[j].Piecearea[k].rings = GetPositionFromRings(ref pps,
                            china.Province[i].City[j].Piecearea[k].name);
                    }
                }
            }

            //XmlHelper.XmlSerializeToFile(china, @"F:\GMap\china-province city2.xml", Encoding.UTF8);
            //JsonHelper.JsonSerializeToFile(china, @"F:\GMap\china-province city", Encoding.UTF8);

            //string file = "chinaBoundry";
            //Country china = GMapChinaRegion.ChinaMapRegion.GetChinaRegionFromJsonFile(file);

            //for (int i = 0; i < china.Province.Count; ++i )
            //{
            //    china.Province[i].rings = EncodeDecodeHelper.CompressString(china.Province[i].rings);
            //    for (int j = 0; j < china.Province[i].City.Count; ++j )
            //    {
            //        china.Province[i].City[j].rings = EncodeDecodeHelper.CompressString(china.Province[i].rings);
            //    }
            //}

            //JsonHelper.JsonSerializeToFile(china,"chinaBoundryEncode",Encoding.UTF8);

            JsonHelper.JsonSerializeToBinaryFile(china, "ChinaBoundryBinaryAll");

            //List<Placemark> placeList = new List<Placemark>();
            //placeList = SoSoMapProvider.Instance.GetPlacemarksByKeywords("东南大学");

            //foreach (var place in placeList)
            //{
            //    Console.WriteLine(place);
            //}

            //PointLatLng position = new PointLatLng(32.043, 118.773);
            //GPoint pos = GMapProviders.GoogleChinaMap.Projection.FromLatLngToPixel(position, 18);
            //PointLatLng position1 = GMapProviders.GoogleChinaMap.Projection.FromPixelToLatLng(pos, 18);
            ////PointLatLng position2 = GMapProvidersExt.Baidu.BaiduMapProvider.Instance.Projection.FromPixelToLatLng(pos, 18);
            //PointLatLng position2 = GMapProvidersExt.Baidu.BaiduProjection.Instance.FromPixelToLatLng(pos, 16);
            //Console.WriteLine(position1.Lng + " " + position1.Lat);
            //Console.WriteLine(position2.Lng + " " + position2.Lat);

            Console.WriteLine("Complete!");
            Console.ReadKey();
        }

        private static Country GetCountryDataFromFile(string file)
        {
            Country country = XmlHelper.XmlDeserializeFromFile<Country>(file, Encoding.UTF8);
            return country;
        }

        private static string GetPositionFromRings(ref string rings,string name)
        {
            if (rings == null)
            {
                Console.WriteLine(name);
                return "";
            }
            string[] res = rings.Split(';');
            return res[0];
        }
    }
}