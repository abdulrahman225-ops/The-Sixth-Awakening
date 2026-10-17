using System;
namespace SixthAwakening {
 public enum HealthState { Healthy, Wounded, Critical, Dead }
 // Engine-independent rules, shared by players, enemies and bosses.
 public sealed class HealthModel {
  public int Maximum { get; private set; }
  public int Current { get; private set; }
  public float Ratio { get { return (float)Current / Maximum; } }
  public bool IsDead { get { return Current == 0; } }
  public HealthState State { get { return IsDead ? HealthState.Dead : Ratio <= .25f ? HealthState.Critical : Ratio <= .5f ? HealthState.Wounded : HealthState.Healthy; } }
  public HealthModel(int maximum) { Reset(maximum); }
  public void Reset(int maximum) { if(maximum<1) throw new ArgumentOutOfRangeException("maximum"); Maximum=maximum; Current=maximum; }
  public int Damage(int amount) { if(amount<=0 || IsDead) return 0; int applied=Math.Min(Current,amount); Current-=applied; return applied; }
  public int Heal(int amount) { if(amount<=0 || IsDead) return 0; int applied=(int)Math.Min((long)Maximum-Current,(long)amount); Current+=applied; return applied; }
 }
}
