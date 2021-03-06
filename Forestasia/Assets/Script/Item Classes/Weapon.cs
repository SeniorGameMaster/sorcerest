public class Weapon : BuffItem {
	private int _maxDamage;
	private float _dmgVar;
	private float _maxRange;
	private DamageType _dmgType;
	
	public Weapon() {
		_maxDamage = 0;
		_dmgVar = 0;
		_maxRange = 0;
		_dmgType = DamageType.Bludgeon;
	}
	
	public Weapon(int mDmg, float dmgV, float mRange, DamageType dt) {
		_maxDamage = mDmg;
		_dmgVar = dmgV;
		_maxRange = mRange;
		_dmgType = dt;
	}
	
	public int MaxDamage {
		get { return _maxDamage; }	
		set { _maxDamage = Point; }	
	}
	
	public float DamageVariance {
		get { return _dmgVar; }	
		set { _dmgVar = Point; }	
	}
	
	public float MaxRange {
		get { return _maxRange; }	
		set { _maxRange = Point; }	
	}
	
	public DamageType TypeOfDamage {
		get { return _dmgType; }	
		set { _dmgType = value; }	
	}
	
	public override string ToString () {
		 return Name + "\n" +
				"Value " + Point + "\n" + 
				"Durability " + CurDurability + "/" + MaxDurability + "\n" +
				MaxDamage * DamageVariance + " - " + MaxDamage; 
	}
}

public enum DamageType {
	Bludgeon,
	Pierce,
	Slash,
	Fire,
	Ice,
	Lightning,
	Acid
}