using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;
    List<Round> nextRounds = new List<Round>();
    List<Round> currentRounds = new List<Round>();
    int nextRoundSeconds = 0;
    int currentRoundSeconds = 0;
    int currentRoundSecondsElapsed = 0;
    float deltaSecond = 0;

    public TextMeshProUGUI clockObject;
    public Transform intermissionLocation;
    public Transform levelLocation;

    

    private void Awake()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;

        
        
    }

    private void Start()
    {
        addRound(new Intermission());
        startRound();
    }

    public void addRound(Round r)
    {
        nextRounds.Add(r);
    }

    public void startRound()
    {
        
        currentRoundSeconds = 0;
        
        
       
        int toLoad = 0;
        foreach(Round r in nextRounds)
        {
           // Debug.Log(r);
           // Debug.Log("toLoad before " + toLoad);
            r.load();
            toLoad += (int)r.getType();
           // Debug.Log("toLoad after " + toLoad);
            nextRoundSeconds += r.getRoundTime();
            currentRounds.Add(r);
        }

        //Debug.Log("toLoad " + toLoad);


        if (nextRoundsHaveIntermission())
        {
            sendPlayersToIntermission();
            
        }
        else
        {
            loadLevel(toLoad);
            sendPlayersToLevel();
        }
        
       
        currentRoundSecondsElapsed = 0;
        currentRoundSeconds = nextRoundSeconds;
        updateScreenClock();
        nextRoundSeconds = 0;
        
        nextRounds.Clear();
        
        EventManager.onSecondTickEvent += secondTick;
        EventManager.onRoundStart?.Invoke(null, System.EventArgs.Empty);
    }

    public void endRound()
    {
        EventManager.onSecondTickEvent -= secondTick;
        EventManager.onRoundEnd?.Invoke(null, System.EventArgs.Empty);

        for (int i = 0; i < GameManager.instance.levelManager.transform.childCount; i++) {
            if (GameManager.instance.levelManager.transform.GetChild(i).gameObject.name.Equals("FallTrigger"))
            {
                continue;
            }
            Destroy(GameManager.instance.levelManager.transform.GetChild(i).gameObject);
        
        }

        foreach (Round r in currentRounds)
        {
            r.unload();
        }
        

        if (!currentRoundsHaveIntermission())
        {

            currentRounds.Clear();//to clear it before next rounds get loaded (but must be available to check for intermission above)
            addRound(new Intermission());
            startRound();
        }
        else
        {
            currentRounds.Clear();
            generateNextRoundLevels();
            startRound();
        }
    }

    private void generateNextRoundLevels()
    {
        List<roundType> playingRounds = new List<roundType>();
        int levelsPerRound = 2;
       
        List<roundType> possibleRounds = new List<roundType>();
        foreach(roundType r in System.Enum.GetValues(typeof(roundType)))
        {
            if(r == roundType.NONE || r == roundType.INTERMISSION)
            {
                continue;
            }
            possibleRounds.Add(r);
        }
        for (int i = 0; i < levelsPerRound; i++)
        {



            int randomIndex = Random.Range(0, possibleRounds.Count);

           
            playingRounds.Add(possibleRounds[randomIndex]);
            possibleRounds.RemoveAt(randomIndex);
        }

       
        foreach (roundType r in playingRounds)
        {
            
            if (r == roundType.BUMPER)
            {
                addRound(new BumperRound());
            }
            if (r == roundType.DODGEBALL)
            {
                addRound(new DodgeballRound());
            }
            if (r == roundType.PACHINKO_BALL)
            {
                addRound(new PachinkoRound());
            }
            if (r == roundType.FALLING_PLATFORMS)
            {
                addRound(new FallingPlatformRound());
            }
        }
    }

    public void loadLevel(int number)
    {
        //Debug.Log(number);
        GameObject level = Instantiate(Resources.Load<GameObject>("Levels/" + number.ToString()));
        level.transform.SetParent(GameManager.instance.levelManager.transform);
        level.transform.position = level.transform.parent.transform.position + level.transform.position;
    }

    protected bool nextRoundsHaveIntermission()
    {
        bool hasIntermission = false;
        
        foreach (Round r in nextRounds)
        {
           
            if (r.getType() == roundType.INTERMISSION)
            {
                hasIntermission = true;
            }
        }

        return hasIntermission;
    }
    protected bool currentRoundsHaveIntermission()
    {
        bool hasIntermission = false;

        foreach (Round r in currentRounds)
        {

            if (r.getType() == roundType.INTERMISSION)
            {
                hasIntermission = true;
            }
        }

        return hasIntermission;
    }

    protected void sendPlayersToLevel()
    {
        sendPlayersToLocation(levelLocation.position);
    }

    protected void sendPlayersToIntermission()
    {
        
        sendPlayersToLocation(intermissionLocation.position);
    }

    protected void sendPlayersToLocation(Vector3 teleportLocation)
    {
       // Debug.Log(GameManager.instance);
        for (int i = 0; i < GameManager.instance.playerManager.transform.childCount; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
            GameManager.instance.playerManager.transform.GetChild(i).transform.position = teleportLocation + offset;
        }
    }
    public void secondTick(object sender, System.EventArgs e)
    {
        currentRoundSecondsElapsed++;
        updateScreenClock();
        if (currentRoundSecondsElapsed == currentRoundSeconds)
        {
            endRound();
        }
       
    }
    

    void updateScreenClock()
    {
        int counter = currentRoundSeconds - currentRoundSecondsElapsed ;
        int minutes = 0;
        int seconds = 0;
        string extraSecondZero = "";
        while (counter> 59)
        {
            counter -= 60;
            minutes++;
        }
        seconds = counter;
        if(seconds < 10)
        {
            extraSecondZero = "0";
        }

         

        clockObject.text = minutes.ToString() + ":" + extraSecondZero + seconds.ToString();
    }
}



