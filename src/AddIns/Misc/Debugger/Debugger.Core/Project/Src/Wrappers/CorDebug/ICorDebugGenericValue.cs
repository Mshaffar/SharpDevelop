// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="David Srbeck�" email="dsrbecky@gmail.com"/>
//     <version>$Revision$</version>
// </file>

#pragma warning disable 1591

namespace Debugger.Interop.CorDebug
{
	using System;
	using System.Runtime.InteropServices;
	
	public static partial class CorDebugExtensionMethods
	{
		public static unsafe Byte[] GetRawValue(this ICorDebugGenericValue corGenVal)
		{
			// TODO: Unset fixing insead
			Byte[] retValue = new Byte[(int)corGenVal.GetSize()];
			IntPtr pValue = Marshal.AllocHGlobal(retValue.Length);
			corGenVal.GetValue(pValue);
			Marshal.Copy(pValue, retValue, 0, retValue.Length);
			Marshal.FreeHGlobal(pValue);
			return retValue;
		}
		
		public static unsafe void SetRawValue(this ICorDebugGenericValue corGenVal, byte[] value)
		{
			if (corGenVal.GetSize() != value.Length) throw new ArgumentException("Incorrect length");
			IntPtr pValue = Marshal.AllocHGlobal(value.Length);
			Marshal.Copy(value, 0, pValue, value.Length);
			corGenVal.SetValue(pValue);
			Marshal.FreeHGlobal(pValue);
		}
		
		public static unsafe object GetValue(this ICorDebugGenericValue corGenVal,Type type)
		{
			object retValue;
			IntPtr pValue = Marshal.AllocHGlobal((int)corGenVal.GetSize());
			corGenVal.GetValue(pValue);
			switch(type.FullName) {
				case "System.Boolean": retValue = *((System.Boolean*)pValue); break;
				case "System.Char":    retValue = *((System.Char*)   pValue); break;
				case "System.SByte":   retValue = *((System.SByte*)  pValue); break;
				case "System.Byte":    retValue = *((System.Byte*)   pValue); break;
				case "System.Int16":   retValue = *((System.Int16*)  pValue); break;
				case "System.UInt16":  retValue = *((System.UInt16*) pValue); break;
				case "System.Int32":   retValue = *((System.Int32*)  pValue); break;
				case "System.UInt32":  retValue = *((System.UInt32*) pValue); break;
				case "System.Int64":   retValue = *((System.Int64*)  pValue); break;
				case "System.UInt64":  retValue = *((System.UInt64*) pValue); break;
				case "System.Single":  retValue = *((System.Single*) pValue); break;
				case "System.Double":  retValue = *((System.Double*) pValue); break;
				case "System.IntPtr":  retValue = *((System.IntPtr*) pValue); break;
				case "System.UIntPtr": retValue = *((System.UIntPtr*)pValue); break;
				default: throw new NotSupportedException();
			}
			Marshal.FreeHGlobal(pValue);
			return retValue;
		}
		
		public static unsafe void SetValue(this ICorDebugGenericValue corGenVal, Type type, object value)
		{
			IntPtr pValue = Marshal.AllocHGlobal((int)corGenVal.GetSize());
			switch(type.FullName) {
				case "System.Boolean": *((System.Boolean*)pValue) = (System.Boolean)value; break;
				case "System.Char":    *((System.Char*)   pValue) = (System.Char)   value; break;
				case "System.SByte":   *((System.SByte*)  pValue) = (System.SByte)  value; break;
				case "System.Byte":    *((System.Byte*)   pValue) = (System.Byte)   value; break;
				case "System.Int16":   *((System.Int16*)  pValue) = (System.Int16)  value; break;
				case "System.UInt16":  *((System.UInt16*) pValue) = (System.UInt16) value; break;
				case "System.Int32":   *((System.Int32*)  pValue) = (System.Int32)  value; break;
				case "System.UInt32":  *((System.UInt32*) pValue) = (System.UInt32) value; break;
				case "System.Int64":   *((System.Int64*)  pValue) = (System.Int64)  value; break;
				case "System.UInt64":  *((System.UInt64*) pValue) = (System.UInt64) value; break;
				case "System.Single":  *((System.Single*) pValue) = (System.Single) value; break;
				case "System.Double":  *((System.Double*) pValue) = (System.Double) value; break;
				case "System.IntPtr":  *((System.IntPtr*) pValue) = (System.IntPtr) value; break;
				case "System.UIntPtr": *((System.UIntPtr*)pValue) = (System.UIntPtr)value; break;
				default: throw new NotSupportedException();
			}
			corGenVal.SetValue(pValue);
			Marshal.FreeHGlobal(pValue);
		}
	}
}

#pragma warning restore 1591
