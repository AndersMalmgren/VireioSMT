
------------------------------------------------------------
////    Vireio SharedMemory Tracker - FreePIE plugin    ////
------------------------------------------------------------

This project is a dll plugin for FreePIE that sends data to Vireio Perception's SharedMemory Tracker.

BUILD INSTRUCTIONS:
---------------------------------
Created using Visual C# 2010 Express on Windows 7 64-bit.
http://www.microsoft.com/visualstudio/eng/downloads#d-2010-express 

FreePIE.Core.Contracts.dll is from FreePIE.

FreePIE can be found at
	http://andersmalmgren.github.com/FreePIE/
forum
	http://www.mtbs3d.com/phpBB/viewforum.php?f=139
	
Vireio Perception can be found at
	http://www.vireio.com/
forum
	http://www.mtbs3d.com/phpBB/viewforum.php?f=141

Install:
	just copy VireioSMT.dll to \FreePIE\plugins\

	VireioFreePIE - TrackIR.py is a sample FreePIE script for TrackIR.
Notes:
	yaw, pitch, and roll are in degrees.
	x,y,z are in centimeters.
	x,y,z are not curently used by Vireio Perception.
