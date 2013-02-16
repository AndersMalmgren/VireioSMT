def update():
	#a vlaue of 0 it straight ahead/no roll
	# no data is sent to Vireio Perception untill .sendData is called
	vireioSMT.yaw = trackIR.yaw
	vireioSMT.pitch = trackIR.pitch
	vireioSMT.roll = trackIR.roll
	vireioSMT.x = trackIR.x / 5.0
	vireioSMT.y = trackIR.y / 5.0
	vireioSMT.z = trackIR.z / 5.0
if starting:
	trackIR.update += update
	