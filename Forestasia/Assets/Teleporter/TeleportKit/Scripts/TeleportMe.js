//http://formation-facile.fr (c) 2011

public var TelepSound : AudioClip;
public var SpawnPoint1_1 : GameObject;		public var SpawnPoint1_2 : GameObject;
public var SpawnPoint2_1 : GameObject;		public var SpawnPoint2_2 : GameObject;
//Teleportation points

function OnTriggerEnter (other : Collider) {
    if (other.gameObject.tag == ("teleporter1_1")) { //Check for collision
	audio.PlayOneShot(TelepSound);
	yield WaitForSeconds(0.8);
	transform.position = SpawnPoint1_2.transform.position; //Teleportation
	yield WaitForSeconds(1.5);
    }
	
	 if (other.gameObject.tag == ("teleporter1_2")) {
	audio.PlayOneShot(TelepSound);
	yield WaitForSeconds(0.8);
	transform.position = SpawnPoint1_1.transform.position;
	yield WaitForSeconds(1.5);
    }
	
	 if (other.gameObject.tag == ("teleporter2_1")) {
	audio.PlayOneShot(TelepSound);
	yield WaitForSeconds(0.8);
	transform.position = SpawnPoint2_2.transform.position;
	yield WaitForSeconds(1.5);
    }
	
	 if (other.gameObject.tag == ("teleporter2_2")) {
	audio.PlayOneShot(TelepSound);
	yield WaitForSeconds(0.8);
	transform.position = SpawnPoint2_1.transform.position;
	yield WaitForSeconds(1.5);
    }
}