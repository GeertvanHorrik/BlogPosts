///////////////////////////////////////////////////////////////////////////////
// CapStructures v1.1
//
// This software is released into the public domain.  You are free to use it
// in any way you like, except that you may not sell this source code.
//
// This software is provided "as is" with no expressed or implied warranty.
// I accept no liability for any damage or loss of business that this software
// may cause.
// 
// This source code is originally written by Tamir Khason (see http://blogs.microsoft.co.il/blogs/tamir
// or http://www.codeplex.com/wpfcap).
// 
// Modifications are made by Geert van Horrik (CatenaLogic, see http://blog.catenalogic.com) 
// Modifications are made by Konstantin, http://const.me
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CatenaLogic.Windows.Presentation.WebcamPlayer
{
	[ComVisible( false )]
	internal enum PinDirection
	{
		Input,
		Output
	}

	// struct AM_MEDIA_TYPE
	[ComVisible( false ), StructLayout( LayoutKind.Sequential )]
	internal class AMMediaType : IDisposable
	{
		public Guid MajorType;

		public Guid SubType;

		[MarshalAs( UnmanagedType.Bool )]
		public bool FixedSizeSamples = true;

		[MarshalAs( UnmanagedType.Bool )]
		public bool TemporalCompression;

		public int SampleSize = 1;

		public Guid FormatType;

		public IntPtr unkPtr;

		public int FormatSize;

		public IntPtr FormatPtr;

		~AMMediaType()
		{
			Dispose( false );
		}

		public void Dispose()
		{
			Dispose( true );
			// remove me from the Finalization queue 
			GC.SuppressFinalize( this );
		}

		protected virtual void Dispose( bool disposing )
		{
			if( FormatSize != 0 )
				Marshal.FreeCoTaskMem( FormatPtr );
			if( unkPtr != IntPtr.Zero )
				Marshal.Release( unkPtr );
		}
	}

	[ComVisible(false), StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
	internal class PinInfo
	{
		public IBaseFilter Filter;

		public PinDirection Direction;

		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 128 )]
		public string Name;
	}

	[ComVisible( false ), StructLayout( LayoutKind.Sequential )]
	internal struct VideoInfoHeader
	{
		public RECT SrcRect;

		public RECT TargetRect;

		public int BitRate;

		public int BitErrorRate;

		public long AverageTimePerFrame;

		public BitmapInfoHeader BmiHeader;
	}
    
	[ComVisible( false ), StructLayout( LayoutKind.Sequential, Pack = 2 )]
	internal struct BitmapInfoHeader
	{
		public int Size;

		public int Width;

		public int Height;

		public short Planes;

		public short BitCount;

		public int Compression;

		public int ImageSize;

		public int XPelsPerMeter;

		public int YPelsPerMeter;

		public int ColorsUsed;

		public int ColorsImportant;
	}

	[ComVisible( false ), StructLayout( LayoutKind.Sequential )]
	internal struct RECT
	{
		public int Left;
		public int Top;
		public int Right;
		public int Bottom;
	}

	[ComVisible( false ), StructLayout( LayoutKind.Sequential )]
	internal struct Size
	{
		public int cx;
		public int cy;

		public override string ToString()
		{
			return String.Format( "{0} × {1}", cx, cy );
		}
	}

	/// <summary>From AnalogVideoStandard</summary>
	/// <remarks>The AnalogVideoStandard enumeration specifies the format of an analog television signal.</remarks>
	[Flags]
	internal enum AnalogVideoStandard
	{
		None = 0x00000000,
		NTSC_M = 0x00000001,
		NTSC_M_J = 0x00000002,
		NTSC_433 = 0x00000004,
		PAL_B = 0x00000010,
		PAL_D = 0x00000020,
		PAL_G = 0x00000040,
		PAL_H = 0x00000080,
		PAL_I = 0x00000100,
		PAL_M = 0x00000200,
		PAL_N = 0x00000400,
		PAL_60 = 0x00000800,
		SECAM_B = 0x00001000,
		SECAM_D = 0x00002000,
		SECAM_G = 0x00004000,
		SECAM_H = 0x00008000,
		SECAM_K = 0x00010000,
		SECAM_K1 = 0x00020000,
		SECAM_L = 0x00040000,
		SECAM_L1 = 0x00080000,
		PAL_N_COMBO = 0x00100000,

		NTSCMask = 0x00000007,
		PALMask = 0x00100FF0,
		SECAMMask = 0x000FF000
	}

	/// <summary>From VIDEO_STREAM_CONFIG_CAPS</summary>
	/// <remarks>The VIDEO_STREAM_CONFIG_CAPS structure describes a range of video formats.
	/// Video compression and video capture filters use this structure to describe what formats they can produce.</remarks>
	[StructLayout( LayoutKind.Sequential )]
	internal class VideoStreamConfigCaps
	{
		public Guid guid;
		public AnalogVideoStandard VideoStandard;
		public Size InputSize;
		public Size MinCroppingSize;
		public Size MaxCroppingSize;
		public int CropGranularityX;
		public int CropGranularityY;
		public int CropAlignX;
		public int CropAlignY;
		public Size MinOutputSize;
		public Size MaxOutputSize;
		public int OutputGranularityX;
		public int OutputGranularityY;
		public int StretchTapsX;
		public int StretchTapsY;
		public int ShrinkTapsX;
		public int ShrinkTapsY;
		public long MinFrameInterval;
		public long MaxFrameInterval;
		public int MinBitsPerSecond;
		public int MaxBitsPerSecond;
	}
}