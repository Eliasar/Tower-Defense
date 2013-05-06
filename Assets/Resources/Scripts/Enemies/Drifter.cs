using UnityEngine;
using System.Collections;

public class Drifter : Enemy {

    public float shotInterval;
    private float nextShot;

    public int rand;
    public Hashtable ht;

	// Use this for initialization
	void Start () {
        HP = 1;
        nextShot = Random.Range(1.0f, 2.0f);
		rand = Random.Range(0, 2);
        ht = new Hashtable();
        ht.Add("time", 3);
        ht.Add("easetype", iTween.EaseType.linear);
        ht.Add("onComplete", "Reset");
        ht.Add("onCompleteTarget", gameObject);
        ht.Add("path", iTweenPath.GetPath("Lane 1"));

		/*switch(rand) {
		case 0:
            ht.Add("path", iTweenPath.GetPath("South Snake"));
			break;
        case 1:
            ht.Add("path", iTweenPath.GetPath("North Snake"));
			break;
		}*/

        iTween.MoveTo(gameObject, ht);
    }

    protected override void Update() {
        /*base.Update();

        shotInterval += Time.deltaTime;
        if (shotInterval >= nextShot) {
            base.Fire();
            shotInterval = 0.0f;
        }*/
    }

    protected override void OnBecameInvisible()
    {
        //base.OnBecameInvisible();
        /*if (spawnTimerGrace >= gracePeriod) {
            //iTween.PutOnPath(gameObject, iTweenPath.GetPath("North Snake"), 0.0f);
            //iTween.MoveTo(gameObject, ht);
        }*/
    }

    void Reset() {
        Vector3[] temp = (Vector3[])ht["path"];
        if(temp == iTweenPath.GetPath("Lane 1")) {
            temp = iTweenPath.GetPath("Lane 2");
        }
        iTween.MoveTo(gameObject, ht);
    }
}
