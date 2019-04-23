using System;
using NAudio.Wave;
using System.IO;

namespace ConsoleMusic
{
	class MainClass
	{
		// Wyjscie Audio
		static WaveOut Audio_Out = new WaveOut();
		// Aktualnie odtwarzany plik audio
		static AudioFileReader Audio_File;
		// Lista Plikow
		static string[] FileList;

		// Zmienne
		static int Focused = 0;
		static int Played = 0;

		public static void Main(string[] args)
		{
			Console.CursorVisible = false;
			Console.SetWindowSize(33, 32);
			Console.BufferWidth = 33;
			Console.BufferHeight = 32;

			// File list
			FileList = Directory.GetFiles("Music");

			// FirstDraw
			Draw.DrawFileList(FileList, Focused,Played);

			// Next Song
			Audio_Out.PlaybackStopped += Audio_Out_PlaybackStopped;
				


			// Obsluga Klawiatury
			while (true)
			{
				switch (Console.ReadKey().Key)
				{
					case ConsoleKey.UpArrow:
						if (Focused > 0)
						{
							Focused--;
						}
						break;

					case ConsoleKey.DownArrow:
						if (Focused < FileList.Length - 1)
						{
							Focused++;
						}
						break;
					case ConsoleKey.Enter:
						Played = Focused;
						PlaySong(FileList[Focused]);
						break;
				}
				Console.Clear();
				Draw.DrawFileList(FileList, Focused,Played);
			}
		}

		static void Audio_Out_PlaybackStopped(object sender, EventArgs e)
		{
			Played++;
			PlaySong(FileList[Focused]);
		}

		static void PlaySong(string file)
		{
			Console.Clear();
			Draw.DrawFileList(FileList, Focused, Played);
			Audio_Out = new WaveOut();
			Audio_File = new AudioFileReader(FileList[Focused]);
			Audio_Out.Init(Audio_File);
			Audio_Out.Play();
		}	                      
	}
}
