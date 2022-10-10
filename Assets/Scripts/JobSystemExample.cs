using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

public class JobSystemExample : MonoBehaviour
{
    void Start()
    {
        DoExample();
    }

    private void DoExample()
    {
        
        //Instantiate and initialize
        NativeArray<float> resultArray = new NativeArray<float>(2,Allocator.TempJob);

        SimpleJob firstJob = new SimpleJob
        {
            x = 12,
            result = resultArray
        };

        SecondJob secondJob = new SecondJob
        {
            result = resultArray
        };
        
        //Schedule
        JobHandle firstJobHandle = firstJob.Schedule();
        JobHandle secondJobHandle = secondJob.Schedule(firstJobHandle);
        
        //Make sure that the job is completed
        //firstJobHandle.Complete();
        
        secondJobHandle.Complete();//As second job depends on the first job, calling complete() function for second job will automatically execute the first job!!

        float resultingValue = resultArray[0];
        Debug.Log(resultingValue);
        Debug.Log(resultArray[1]);

        //Dispose the result array in order to prevent memory leak
        resultArray.Dispose();

    }

    private struct SimpleJob : IJob
    {
        public float x;
        public NativeArray<float> result;

        public void Execute()
        {
            result[0] = x;
            x++;
            result[1] = x;
        }
    }

    private struct SecondJob : IJob
    {
        public NativeArray<float> result;

        public void Execute()
        {
            result[0] = result[0] + 1;
        }
    }
}
