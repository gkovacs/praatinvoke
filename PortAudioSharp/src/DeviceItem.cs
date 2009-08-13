 /*
  * PortAudioSharp - PortAudio bindings for .NET
  * Copyright 2006, 2007, 2008 Riccardo Gerosa and individual contributors as indicated
  * by the @authors tag. See the copyright.txt in the distribution for a
  * full listing of individual contributors.
  *
  * This is free software; you can redistribute it and/or modify it
  * under the terms of the GNU Lesser General Public License as
  * published by the Free Software Foundation; either version 2.1 of
  * the License, or (at your option) any later version.
  *
  * This software is distributed in the hope that it will be useful,
  * but WITHOUT ANY WARRANTY; without even the implied warranty of
  * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
  * Lesser General Public License for more details.
  *
  * You should have received a copy of the GNU Lesser General Public
  * License along with this software; if not, write to the Free
  * Software Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA
  * 02110-1301 USA, or see the FSF site: http://www.fsf.org.
  */
 
using System;

namespace PortAudioSharp.PortAudioSharp
{

	public class DeviceItem
	{
		private int deviceIndex;
		private PortAudio.PaDeviceInfo deviceInfo;
		
		public int DeviceIndex
		{
			get { return deviceIndex; }
		}
		
		public PortAudio.PaDeviceInfo DeviceInfo 
		{
			get { return deviceInfo; }
		}
			
		public DeviceItem(int deviceIndex, PortAudio.PaDeviceInfo deviceInfo) 
		{
			this.deviceIndex = deviceIndex;
			this.deviceInfo = deviceInfo;
		}
		
		public override string ToString()
		{
			return deviceInfo.name;
		}
	}
	
}
