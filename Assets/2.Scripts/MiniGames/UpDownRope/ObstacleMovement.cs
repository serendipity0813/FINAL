using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private UpDownRopeGame upDownRopeGame;
    private Vector3 m_targetPosition;
    private float m_speed;

    private void Awake()
    {
        upDownRopeGame = transform.parent.GetComponent<UpDownRopeGame>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        
        if(Random.Range(0, 2) == 1)
        {
            m_targetPosition.x = Random.Range(9.0f, 12.0f);
        }
        else
        {
            m_targetPosition.x = Random.Range(-9.0f, -12.0f);
        }
        m_targetPosition.y = Random.Range(-31, -5);
        m_targetPosition.z = -1;
        m_speed = Random.Range(0.05f, 0.1f) + upDownRopeGame.difficulty * 0.05f;
        gameObject.transform.position = m_targetPosition;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (gameObject.transform.position == m_targetPosition)
        {
            EffectSoundManager.Instance.PlayEffect(26);
            m_targetPosition.x = -m_targetPosition.x;
            //birdRenderer.flipX = !birdRenderer.flipX;
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_targetPosition, m_speed);
            gameObject.transform.Rotate(new Vector3(0, 0, 5));
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                EffectSoundManager.Instance.PlayEffect(18);
                upDownRopeGame.clearCount--;
            }
        }
    }
}
