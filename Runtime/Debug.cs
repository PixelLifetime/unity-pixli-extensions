using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Object = UnityEngine.Object;
using UDebug = UnityEngine.Debug;

namespace PixLi.Debugging
{
	public class Debug : ILogger
	{
		[Conditional("GLOBAL_DEBUG")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Log(string message)
		{
			UDebug.Log(message);
		}

		[Conditional("GLOBAL_DEBUG")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Log(string message, Object context)
		{
			UDebug.Log(message, context);
		}

		[Conditional("GLOBAL_DEBUG")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void LogWarning(string message)
		{
			UDebug.LogWarning(message);
		}

		[Conditional("GLOBAL_DEBUG")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void LogWarning(string message, Object context)
		{
			UDebug.LogWarning(message, context);
		}

		[Conditional("GLOBAL_DEBUG")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void LogError(string message)
		{
			UDebug.LogError(message);
		}

		[Conditional("GLOBAL_DEBUG")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void LogError(string message, Object context)
		{
			UDebug.LogError(message, context);
		}

		public bool IsLogTypeAllowed(LogType logType)
		{
			throw new NotImplementedException();
		}

		public void Log(LogType logType, object message)
		{
			throw new NotImplementedException();
		}

		public void Log(LogType logType, object message, Object context)
		{
			throw new NotImplementedException();
		}

		public void Log(LogType logType, string tag, object message)
		{
			throw new NotImplementedException();
		}

		public void Log(LogType logType, string tag, object message, Object context)
		{
			throw new NotImplementedException();
		}

		public void Log(object message)
		{
			throw new NotImplementedException();
		}

		public void Log(string tag, object message)
		{
			throw new NotImplementedException();
		}

		public void Log(string tag, object message, Object context)
		{
			throw new NotImplementedException();
		}

		public void LogWarning(string tag, object message)
		{
			throw new NotImplementedException();
		}

		public void LogWarning(string tag, object message, Object context)
		{
			throw new NotImplementedException();
		}

		public void LogError(string tag, object message)
		{
			throw new NotImplementedException();
		}

		public void LogError(string tag, object message, Object context)
		{
			throw new NotImplementedException();
		}

		public void LogFormat(LogType logType, string format, params object[] args)
		{
			throw new NotImplementedException();
		}

		public void LogException(Exception exception)
		{
			throw new NotImplementedException();
		}

		public ILogHandler logHandler
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		public bool logEnabled
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		public LogType filterLogType
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		public void LogFormat(LogType logType, Object context, string format, params object[] args)
		{
			throw new NotImplementedException();
		}

		public void LogException(Exception exception, Object context)
		{
			throw new NotImplementedException();
		}

#if UNITY_EDITOR
#endif
	}
}