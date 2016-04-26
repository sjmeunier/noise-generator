using System;
using Microsoft.DirectX.DirectSound;

namespace NoiseGenerator
{
	/// <summary>
	/// Summary description for NoiseGen.
	/// </summary>
	public class NoiseGen
	{
		public int BitsPerSample;
		public int Channels;
		public int BlockAlign;
		public int SamplesPerSecond;
		public int BufferSize;
		static public Device applicationDevice;

		private WaveFormat format;   
		private BufferDescription desc;
		private SecondaryBuffer secondaryBuffer;

		public NoiseGen(System.IntPtr obj)
		{
			BitsPerSample = 8;
			Channels = 1;
			BlockAlign = 1;
			SamplesPerSecond = 44000;
			BufferSize = 132000;

			applicationDevice = new Device();
			applicationDevice.SetCooperativeLevel(obj, CooperativeLevel.Normal);
			
			SetUpSound();
		}

		private void SetUpSound()
		{
			format = new WaveFormat();
			format.BitsPerSample = (short)BitsPerSample;         
			format.Channels = (short)Channels;         
			format.BlockAlign = (short)BlockAlign;         

			format.FormatTag = WaveFormatTag.Pcm;         
			format.SamplesPerSecond = SamplesPerSecond; //sampling frequency of your data;   
			format.AverageBytesPerSecond = format.SamplesPerSecond * format.BlockAlign;       


			desc = new BufferDescription(format);
			desc.DeferLocation = true;
			desc.BufferBytes = BufferSize;   
		}
		
		public void Stop()
		{
			secondaryBuffer.Stop();
			
		}
		
		public void Generate(string WaveType, double Frequency)
		{
			
			secondaryBuffer = new SecondaryBuffer(desc,applicationDevice);      

			byte[] RawSamples = new byte[BufferSize];
	
			switch(WaveType)
			{
				case "White Noise":
					GenerateWhiteNoise(ref RawSamples, BufferSize);
					break;
				case "Sine Wave":
					GenerateSine(ref RawSamples, BufferSize, Frequency, SamplesPerSecond);
					break;
				case "Square Wave":
					GenerateSquare(ref RawSamples, BufferSize, Frequency, SamplesPerSecond);
					break;
				case "Sawtooth Wave":
					GenerateSawtooth(ref RawSamples, BufferSize, Frequency, SamplesPerSecond);
					break;
				default:
					GenerateWhiteNoise(ref RawSamples, BufferSize);
					break;
			}


			secondaryBuffer.Write(0,RawSamples, LockFlag.EntireBuffer);
			secondaryBuffer.Play(0,BufferPlayFlags.Looping);
			
			
		}

		private void GenerateWhiteNoise(ref byte[] SampleBuffer, int NumSamples)
		{
			Random rnd1 = new System.Random();
      
			for (int i = 0; i < NumSamples; i++)
			{
				SampleBuffer[i] = (byte) rnd1.Next(255);
			}
		}

		private void GenerateSine(ref byte[] SampleBuffer, int NumSamples, double Frequency, double SampleRate)
		{
			int SamplesPerWave = (int)(SampleRate / Frequency );
			double RadPerSample = (Math.PI * 2) / (double) SamplesPerWave;
			double SinVal = 0;

			for (int i = 0; i < NumSamples; i++)
			{
				SinVal = Math.Sin(RadPerSample * (double)(i % SamplesPerWave));
				SampleBuffer[i] = (byte) (Math.Floor(SinVal * 127) + 128);
			}
		}

		private void GenerateSquare(ref byte[] SampleBuffer, int NumSamples, double Frequency, double SampleRate)
		{
			int SamplesPerWave = (int)(SampleRate / (Frequency * 2));

			int Value = 127;
			int Counter = 0;
			for (int i = 0; i < NumSamples; i++)
			{
				if (Counter >= SamplesPerWave)
				{
					Counter = 0;
					Value *= -1;
				}
				SampleBuffer[i] = (byte) (Value + 127);
				Counter++;
			}
		}

		private void GenerateSawtooth(ref byte[] SampleBuffer, int NumSamples, double Frequency, double SampleRate)
		{
			int SamplesPerWave = (int)(SampleRate / Frequency);

			int Value = 0;
			int Counter = 0;
			double Delta = 255.0 / (double)SamplesPerWave;
			for (int i = 0; i < NumSamples; i++)
			{
				if (Counter >= SamplesPerWave)
				{
					Counter = 0;
					Value = 0;
				}
				else
				{
					Value += (int)Math.Round(Delta, 0);
					if (Value > 255)
					{
						Value = 255;
					}
				}
				SampleBuffer[i] = (byte) (Value);
				Counter++;
			}
		}

	}
}
