using UnityEngine;
using UnityEngine.UI;
namespace SixthAwakening {
 public sealed class HealthBarView : MonoBehaviour {
  [SerializeField] private HealthComponent target;
  [SerializeField] private Image fill;
  [SerializeField] private Text valueLabel;
  [SerializeField] private string displayName="Kael";
  [SerializeField] private Color healthy=new Color(.65f,.12f,.17f);
  [SerializeField] private Color wounded=new Color(.88f,.49f,.16f);
  [SerializeField] private Color critical=new Color(1f,.18f,.15f);
  private HealthComponent subscribed;
  public void Bind(HealthComponent next){Unsubscribe();target=next;if(isActiveAndEnabled)Subscribe();Refresh(target);}
  private void OnEnable(){Subscribe();Refresh(target);}
  private void OnDisable(){Unsubscribe();}
  private void Subscribe(){if(target!=null&&subscribed!=target){Unsubscribe();subscribed=target;subscribed.Changed+=Refresh;}}
  private void Unsubscribe(){if(subscribed!=null)subscribed.Changed-=Refresh;subscribed=null;}
  private void Refresh(HealthComponent health){
   if(fill==null)return;
   fill.type=Image.Type.Filled;fill.fillMethod=Image.FillMethod.Horizontal;fill.fillOrigin=0;
   fill.fillAmount=health==null?0:health.Ratio;
   fill.color=health==null||health.State==HealthState.Dead?Color.gray:health.State==HealthState.Critical?critical:health.State==HealthState.Wounded?wounded:healthy;
   if(valueLabel!=null)valueLabel.text=health==null?displayName+" — UNBOUND":displayName+"  "+health.Current+" / "+health.Maximum+"  · "+health.State.ToString().ToUpperInvariant();
  }
 }
}
