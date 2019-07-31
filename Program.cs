using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;

namespace VisualPlugin_Copies
{
	class Version{
		public string Shape { get; set; }
		public string SubVersion { get; set; }
		public override string ToString()=>Shape+" "+SubVersion;
	}
	class Program
	{
		static void Main(string[] args){
			var t=new DirectoryInfo(Directory.GetCurrentDirectory()).GetFiles();
			//var L=new List<Version>();
			var cache=new Dictionary<string,string>();
			foreach (var f in t)
				if(f.Name.EndsWith(".rbxm")){
					var s=f.OpenText();
					var l=s.ReadToEnd();
					l=l.Replace("\n","");
					s.Close();
					var r=Regex.Match(l,"shape=[\n]?'?([^,']+)'?,sub='?(.+?)'?}");
					if(r.Index==0)continue;
					var v=new Version{Shape=r.Groups[1].Value,SubVersion=r.Groups[2].Value.Replace("\\","")};
					/*
					var b=true;foreach(var i in L)
						if(i.Shape==v.Shape&&i.SubVersion==v.SubVersion)
							{b=false;break;};
					if(b)L.Add(v);*/
					if(!cache.ContainsKey(v.Shape)){
						Console.WriteLine(v.Shape);
						cache[v.Shape]=Console.ReadLine();
						Console.WriteLine();
					}
					File.Copy(f.Name,$"{f.Name.Substring(0,32)} - {cache[v.Shape]} - {v.SubVersion.Replace("'","’").Replace("\"","”")} .rbxm",true);
				}
				/*
				foreach (var v in L)
					Console.WriteLine(v);
					*/
		}
	}
}
