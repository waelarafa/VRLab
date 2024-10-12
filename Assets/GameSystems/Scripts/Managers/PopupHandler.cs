using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupHandler : IGlobalSingleton<PopupHandler>
{
    [SerializeField] private ModalWindowManager modalWindowManager;

    public void TaskFinished(GameTask gameTask)
    {
      
        modalWindowManager.titleText = "TASK FINISHED";
        modalWindowManager.descriptionText = gameTask.taskTitle;
        modalWindowManager.Open();
        modalWindowManager.UpdateUI();
        modalWindowManager.onConfirm.RemoveAllListeners();
        modalWindowManager.onConfirm.AddListener(() => {
            gameTask.DisableTaskListItem();
            modalWindowManager.Close();
            
        });
    }
}