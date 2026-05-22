using System.Collections;
using UnityEngine;

public class StageSpawnScript : MonoBehaviour
{
    public UnitSpawner unitSpawner;

    
    [System.Serializable]
    public class SpawnData
    {
        // 어떤 유닛을
        public GameObject unitPrefab;
        // 몇 마리
        public int count;
        // 같은 웨이브 내에서 유닛 생성 간격
        public float interval;
        // 다른 웨이브 시작 전 간격
        public float delay;

        //예시)
        //Element 0 근접유닛 count = 3 interval = 1 delay = 3
        //Element 1 원거리유닛 count = 2 interval = 2 delay = 5
        //3초대기, 근접1, 1초대기, 근접1, 1초대기, 근접1, 5초대기, 원거리1, 2초대기, 원거리2 다시 처음부터.
    }   

    public SpawnData[] stage1List;
    public SpawnData[] stage2List;
    public SpawnData[] stage3List;
    public SpawnData[] stage4List;
    public SpawnData[] stage5List;
    public SpawnData[] stage6List;

    public void SpawnStageUnit(int stageParameter)
    {
        switch (stageParameter)
        {
            case 1:
                StartCoroutine(SpawnRoutine(stage1List));
                break;

            case 2:
                StartCoroutine(SpawnRoutine(stage2List));
                break;

            case 3:
                StartCoroutine(SpawnRoutine(stage3List));
                break;

            case 4:
                StartCoroutine(SpawnRoutine(stage4List));
                break;

            case 5:
                StartCoroutine(SpawnRoutine(stage5List));
                break;

            case 6:
                StartCoroutine(SpawnRoutine(stage6List));
                break;

            default:
                Debug.LogError("StageParameter 오류: " + stageParameter);
                break;
        }
    }

    private IEnumerator SpawnRoutine(SpawnData[] list)
    {
        while (true)
        {
            foreach (SpawnData data in list)
            {
                yield return new WaitForSeconds(Mathf.Max(data.delay, 0.1f));

                for (int i = 0; i < data.count; i++)
                {
                    unitSpawner.SpawnUnit(data.unitPrefab);

                    yield return new WaitForSeconds(Mathf.Max(data.interval, 0.1f));
                }
            }
        }
    }
}