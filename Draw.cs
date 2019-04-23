using System;
using System.IO;
using System.Collections.Generic;
using NAudio.Wave;

namespace ConsoleMusic
{
	public class Draw
	{
		static char[] BOX = { '╔','╗','╚','╝','║','═' };

		public static void DrawFileList(string[] file,int selected,int played)
		{
			
			// == Definicje ====================================
			int max_height = 32;
			int max_width = 32;

			// Kolory
			ConsoleColor borderColor = ConsoleColor.White;
			ConsoleColor selectedColorFron = ConsoleColor.Black;
			ConsoleColor selectedColorBack = ConsoleColor.White;
			ConsoleColor playedColorFront = ConsoleColor.Cyan;
			ConsoleColor playedColorBack = ConsoleColor.Black;

			// == Rysowanie Ramki ==============================
			Console.ForegroundColor = borderColor;
			Console.Write(BOX[0]);
			for (int i = 0; i < max_width - 2; i++) {
				Console.Write(BOX[5]);
			}
			Console.Write(BOX[1] + "\n");

			// == Tworzenie Listy ==============================
			string[] fileList = FormatList(file, max_height, selected);

			Console.ForegroundColor = borderColor;
			for (int i = 0; i < max_height - 2; i++) {
				Console.Write(BOX[4]);
				Console.ResetColor();

				if (fileList.Length > i)
				{
					if (i == played)
					{
						Console.ForegroundColor = playedColorFront;
						Console.BackgroundColor = playedColorBack;
					}

					if (i == selected)
					{
						Console.ForegroundColor = selectedColorFron;
						Console.BackgroundColor = selectedColorBack;
					}

					if (selected == played && i == selected)
					{
						Console.ForegroundColor = ConsoleColor.DarkCyan;
						Console.BackgroundColor = selectedColorBack;
					}

					Console.Write(ScrName(fileList[i], max_width));
					Console.ResetColor();
				}
				else
				{
					for (int k = 0; k < max_width - 2; k++)
					{
						Console.Write(' ');
					}
				}

				Console.ForegroundColor = borderColor;
				Console.Write(BOX[4] + "\n");
			}

			Console.Write(BOX[2]);
			for (int i = 0; i < max_width - 2; i++) {
				Console.Write(BOX[5]);
			}
			Console.Write(BOX[3]);
			Console.ResetColor();
		}

		static string ScrName(string file,int max)
		{
			string ext = Path.GetExtension(file);
			string name = Path.GetFileName(file).Trim(ext.ToCharArray());

			if (name.Length == max - 2)
			{
				return name;
			}
			else if (name.Length < max - 2)
			{
				int spaces = max -2 - name.Length;
				for (int i = 0; i < spaces; i++)
				{
					name += " ";
				}
				return name;
			}
			else if (name.Length > max - 2)
			{
				return name.Substring(0,max -4) + "..";
			}
			else
			{
				return name;
			}

		}

		static string[] FormatList(string[] files, int height, int selected)
		{
			List<string> newList = new List<string>();

			if (selected < height - 2)
			{
				for (int i = 0; i < height - 2; i++)
				{
					try
					{
						newList.Add(files[i]);
					}
					catch
					{
						
					}
				}
			}

			if (selected > height - 3)
			{
				for (int i = 0; i < height - 3; i++)
				{
					newList.Add(files[i + 1]);
				}
			}

			return newList.ToArray();
		}
	}
}
