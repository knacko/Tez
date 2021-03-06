public class AdjacentObject {

	private float angle;
	private float range;
	private float rotation;
	private string shape = null;
	
	public AdjacentObject(float angle, float range, float rotation) {
		this.angle = angle;
		this.range = range;
		this.rotation = rotation;
	}	
	/// <summary>
	/// Initializes a new instance of the <see cref="AdjacentObject"/> class.
	/// </summary>
	/// <param name='shape'>
	/// String - must match the prefab name of the target object
	/// </param>
	/// <param name='angle'>
	/// Angle - The angle from the spawning object in which to spawn at
	/// </param>
	/// <param name='range'>
	/// Float - The distance from the center of the spawning object
	/// </param>
	/// <param name='rotation'>
	/// Float - the rotation of the object being spawned
	/// </param>
	public AdjacentObject(string shape, float angle, float range, float rotation) {
		this.shape = shape;
		this.angle = angle;
		this.range = range;
		this.rotation = rotation;
	}	
	
	public string getShape() {
		return shape;
	}
	
	public float getAngle() {
		return angle;	
	}
	
	public float getRange() {
		return range;	
	}
	
	public float getRotation() {
		return rotation;	
	}
	
	public void incAngle(float rot) {
		angle = angle + rot;
	}

}
	