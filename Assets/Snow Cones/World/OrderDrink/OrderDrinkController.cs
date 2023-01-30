using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class OrderDrinkController : MonoBase
{

    public static OrderDrinkController Instance;
    public Transform[] slotPositions;
    public ConeAtBar[] patronSlots = new ConeAtBar[5];
    public ConeAtBar player;
    public List<ConeAtBar> patrons;
    public Queue<ConeAtBar> otherConesAtBar = new Queue<ConeAtBar>();
    public List<ConeAtBar> otherConesNotAtBar = new List<ConeAtBar>();
    public BarTender barTender;
    private int playerSlot;


    // Use this for initialization
    private void Awake()
    {
        Instance = this;

        SetPatronPos(patrons[0], 0);
        SetPatronPos(patrons[1], Random.Range(1, 3));

        int playerSlot = GetEmptySlot(-1);
        SetPatronPos(player, playerSlot);

        for (int i = 0; i < 5; i++)
        {
            if (patronSlots[i] == null)
            {
                barTender.transform.SetX(slotPositions[i].position.x + 100);
            }
        }


        //  StartCoroutine(ShuffleConesAroundRoutine());
    }

    private void Start()
    {

        patrons[1].MoveOffScreen();
    }

    private IEnumerator BarTenderRoutine()
    {
        while (true)
        {


            yield return null;
        }
    }

    public void MoveOutOfSlot(ConeAtBar cone)
    {
        for (int i = 0; i < 5; i++)
        {
            if (patronSlots[i] == cone)
            {
                patronSlots[i] = null;
            }
        }


        if (otherConesNotAtBar.Contains(cone))
            otherConesNotAtBar.Add(cone);
    }


    public ConeAtBar GetThisrtyPatron()
    {
        if (otherConesAtBar.Count > 0)
            return otherConesAtBar.Dequeue();

        return null;
    }


    public void SetPatronPos(ConeAtBar patron, int index)
    {

        patron.transform.position = ClaimSlot(patron, index);

    }

    public Vector3 ClaimSlot(ConeAtBar patron, int index)
    {
        for (int i = 0; i < 5; i++)
        {
            if (patronSlots[i] == patron)
            {
                patronSlots[i] = null;
            }
        }


        patronSlots[index] = patron;

        if (patron == player)
        {
            playerSlot = index;
        }
        else
        {

            if (otherConesAtBar.Contains(patron) == false)
                otherConesAtBar.Enqueue(patron);
        }

        if (otherConesNotAtBar.Contains(patron))
            otherConesNotAtBar.Remove(patron);

        patron.lastSlot = index;
        return slotPositions[index].position;

    }


    public int GetEmptySlot(int notThisSlot)
    {
        int index = Random.Range(0, 4);

        for (int i = 0; i < 5; i++)
        {

            int j = index + i;
            j %= 5;

            if (j != notThisSlot && patronSlots[j] == null)
                return j;

        }

        return 0;
    }


    public int GetEmptySlot(int start, int direction)
    {
        int index = start;

        for (int i = 0; i < 5; i++)
        {

            int j = index + i*direction;
            j = Mathf.Clamp(j, 0, 4);
            print(j + "  " + patronSlots[j]);
            if (patronSlots[j] == null)
                return j;

        }

        return -1;
    }

    private IEnumerator ShuffleConesAroundRoutine()
    {
        while (true)
        {
            bool noOneMoving = false;

            foreach (ConeAtBar patronSlot in patronSlots)
            {
                if (patronSlot != null)
                {
                    if (patronSlot.moving)
                        noOneMoving = false;

                }
            }

            if (noOneMoving)
            {

            }

            yield return null;
        }

    }

    // Update is called once per frame
    private void Update()
    {

        if (player.gettingServed == false && player.moving == false)
        {
            if (LeftDown)
            {
                print("LeftDown ");
                int newSlot = GetEmptySlot(playerSlot, -1);
                if (newSlot >= 0)
                {
                    Vector3 pos = ClaimSlot(player, newSlot);
                    player.MoveToPosition(pos);
                }
            }

            if (RightDown)
            {
                print("RightDown ");
                int newSlot = GetEmptySlot(playerSlot, 1);
                if (newSlot >= 0)
                {
                    Vector3 pos = ClaimSlot(player, newSlot);
                    player.MoveToPosition(pos);
                }
            }
        }
    }
}
