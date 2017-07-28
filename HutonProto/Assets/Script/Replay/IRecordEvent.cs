using UnityEngine.EventSystems;

public interface IRecordEvent : IEventSystemHandler
{
    void OnRecord();
    void OnReplay();
    void OnStop();
}