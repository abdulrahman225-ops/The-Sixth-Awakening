using System;
using UnityEngine;
using UnityEngine.Events;
namespace SixthAwakening {
 [DisallowMultipleComponent]
 public sealed class HealthComponent : MonoBehaviour {
  [SerializeField, Min(1)] private int maximumHealth=100;
  [SerializeField, Min(0)] private float invulnerabilitySeconds=.35f;
  [SerializeField] private UnityEvent onDamaged=new UnityEvent();
  [SerializeField] private UnityEvent onHealed=new UnityEvent();
  [SerializeField] private UnityEvent onDied=new UnityEvent();
  public event Action<HealthComponent> Changed;
  private HealthModel model;
  private float vulnerableAt;
  private void Ensure(){if(model==null)model=new HealthModel(Mathf.Max(1,maximumHealth));}
  public int Current { get { Ensure();return model.Current; } }
  public int Maximum { get { Ensure();return model.Maximum; } }
  public float Ratio { get { Ensure();return model.Ratio; } }
  public HealthState State { get { Ensure();return model.State; } }
  public bool IsDead { get { Ensure();return model.IsDead; } }
  private void Awake(){Ensure();}
  private void OnValidate(){maximumHealth=Mathf.Max(1,maximumHealth);invulnerabilitySeconds=Mathf.Max(0,invulnerabilitySeconds);}
  // Call once per confirmed hit, not every rendered frame. Block/parry logic runs before this.
  public bool ApplyDamage(int amount){
   Ensure();if(amount<=0 || model.IsDead || Time.time<vulnerableAt)return false;
   bool wasAlive=!model.IsDead;
   if(model.Damage(amount)==0)return false;
   vulnerableAt=Time.time+invulnerabilitySeconds;
   bool died=wasAlive&&model.IsDead;
   if(Changed!=null)Changed(this);
   onDamaged.Invoke();
   if(died)onDied.Invoke();
   return true;
  }
  public bool Heal(int amount){Ensure();if(model.Heal(amount)==0)return false;if(Changed!=null)Changed(this);onHealed.Invoke();return true;}
  // Explicit checkpoint/new-life action. Healing never revives a dead actor.
  public void ResetHealth(){Ensure();model.Reset(Mathf.Max(1,maximumHealth));vulnerableAt=0;if(Changed!=null)Changed(this);}
 }
}
