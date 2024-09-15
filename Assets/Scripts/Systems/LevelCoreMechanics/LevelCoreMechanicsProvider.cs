using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelCoreMechanicsProvider : IInitializable, IDisposable
{
    private LevelCoreMechanicsProviderConfig LevelCoreMechanicsProviderConfig;
    private GameCycleEventHandler GameCycleEventHandler;
    private AudioPlayer AudioPlayer;
    private Placeholder[] Placeholders;
    private int PlaceholdersChosenNumber;
    private bool IsMovingNow = false;
    private int PlaceholderWithFlyingObject = -1;
    private int NextPlaceholder = -1;
    private List<Action> OnClickOnPlaceholders = new();
    private List<List<Item>> CurrentPosition = new();

    public LevelCoreMechanicsProvider(LevelCoreMechanicsProviderConfig levelCoreMechanicsProviderConfig, GameCycleEventHandler gameCycleEventHandler, AudioPlayer audioPlayer, Placeholder[] placeholders)
    {
        GameCycleEventHandler = gameCycleEventHandler;
        AudioPlayer = audioPlayer;
        LevelCoreMechanicsProviderConfig = levelCoreMechanicsProviderConfig;
        Placeholders = placeholders;
    }

    public void Initialize()
    {
        for (int i = 0; i < Placeholders.Length; i++)
        {
            CurrentPosition.Add(new());
        }
        PlaceholdersChosenNumber = new System.Random().Next(LevelCoreMechanicsProviderConfig.MinimumPlaceholders, LevelCoreMechanicsProviderConfig.MaximumPlaceholders + 1);
        for (int i = PlaceholdersChosenNumber; i < Placeholders.Length; i++)
        {
            Placeholders[i].Collider.gameObject.SetActive(false);
        }
        SpawnItems();

        foreach (Placeholder placeholder in Placeholders)
        {
            Action OnClickOnThisPlaceholder = () => SelectPlaceholder(placeholder.Config.ID);
            placeholder.OnClick += OnClickOnThisPlaceholder;
            OnClickOnPlaceholders.Add(OnClickOnThisPlaceholder);
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < Placeholders.Length; i++)
        {
            Placeholders[i].OnClick -= OnClickOnPlaceholders[i];
        }
    }

    public void SpawnItems()
    {
        for (int i = 0; i < Placeholders.Length; i++)
        {
            CurrentPosition[i].Clear();
        }
        System.Random Random = new();
        int PlaceholderWithMinimumItems = Random.Next(PlaceholdersChosenNumber);
        for (int i = 0; i < PlaceholdersChosenNumber; i++)
        {
            float YPositionOfThisItem = LevelCoreMechanicsProviderConfig.YPositionOfNewItems;
            float TargetYPositionOfThisItem = LevelCoreMechanicsProviderConfig.LowestYPositionInGlass;
            int ItemsToBeSpawnedForThisPlaceholder = i == PlaceholderWithMinimumItems ? LevelCoreMechanicsProviderConfig.MinimumItemsToBeSpawnedPerPlaceholder : Random.Next(LevelCoreMechanicsProviderConfig.MinimumItemsToBeSpawnedPerPlaceholder, LevelCoreMechanicsProviderConfig.MaximumItemsToBeSpawnedPerPlaceholder + 1);
            int PreviousItemIndex = -1;
            for (int j = 0; j < ItemsToBeSpawnedForThisPlaceholder; j++)
            {
                int ThisItemIndex;
                if (PreviousItemIndex == -1)
                {
                    ThisItemIndex = Random.Next(LevelCoreMechanicsProviderConfig.PossibleItems.Count);
                }
                else
                {
                    ThisItemIndex = Random.Next(LevelCoreMechanicsProviderConfig.PossibleItems.Count - 1);
                    if (ThisItemIndex >= PreviousItemIndex)
                    {
                        ThisItemIndex++;
                    }
                }
                PreviousItemIndex = ThisItemIndex;
                GameObject NewItemGameObject = UnityEngine.Object.Instantiate(LevelCoreMechanicsProviderConfig.PossibleItems[ThisItemIndex].ItemPrefab);
                Item NewItem = new(LevelCoreMechanicsProviderConfig.PossibleItems[ThisItemIndex].Config, NewItemGameObject);
                Vector3 ThisPlaceholderPosition = Placeholders[i].Collider.transform.position;
                NewItemGameObject.transform.position = new(ThisPlaceholderPosition.x + NewItem.Config.Offset.x, YPositionOfThisItem, ThisPlaceholderPosition.z + NewItem.Config.Offset.z);
                NewItemGameObject.transform.DOMove(new(NewItemGameObject.transform.position.x, TargetYPositionOfThisItem, NewItemGameObject.transform.position.z), LevelCoreMechanicsProviderConfig.DurationOfMovingUp).OnComplete(() => NewItem.InitializeStartY());
                YPositionOfThisItem += LevelCoreMechanicsProviderConfig.PossibleItems[ThisItemIndex].Config.Height;
                TargetYPositionOfThisItem += LevelCoreMechanicsProviderConfig.PossibleItems[ThisItemIndex].Config.Height;
                CurrentPosition[i].Add(NewItem);
            }
        }
    }

    public void SelectPlaceholder(int ID)
    {
        if (GameCycleEventHandler.CurrentGameState == GameState.Finished) return;

        for (int i = 0; i < CurrentPosition.Count; i++)
        {
            for (int j = 0; j < CurrentPosition[i].Count; j++)
            {
                if (CurrentPosition[i].Count != 0 && !CurrentPosition[i][0].WasStartYGot)
                {
                    return;
                }
            }
        }

        if (!IsMovingNow)
        {
            if (PlaceholderWithFlyingObject == -1)
            {
                if (CurrentPosition[ID].Count != 0)
                {
                    IsMovingNow = true;
                    Vector3 EndPoint = CurrentPosition[ID][^1].InstanceGameObject.transform.position;
                    EndPoint.y = LevelCoreMechanicsProviderConfig.YPositionOfNewItems;
                    PlayWhooshSound();
                    CurrentPosition[ID][^1].InstanceGameObject.transform.DOMove(EndPoint, LevelCoreMechanicsProviderConfig.DurationOfMovingUp).OnComplete(() =>
                    {
                        PlaceholderWithFlyingObject = ID;
                        StopMoving();
                    }
                    );
                }
            }
            else if (PlaceholderWithFlyingObject != ID)
            {
                if (CurrentPosition[ID].Count < LevelCoreMechanicsProviderConfig.MaximumItemsInPlaceholder)
                {
                    IsMovingNow = true;
                    Vector3 StartPoint = CurrentPosition[PlaceholderWithFlyingObject][^1].InstanceGameObject.transform.position;
                    Vector3 EndPoint = Placeholders[ID].Collider.transform.position + CurrentPosition[PlaceholderWithFlyingObject][^1].Config.Offset;
                    EndPoint.y = LevelCoreMechanicsProviderConfig.YPositionOfNewItems;
                    Vector3 MiddlePoint = (StartPoint + EndPoint) / 2 + Vector3.up * (StartPoint - EndPoint).magnitude / 2;
                    Vector3[] Path = new Vector3[] { StartPoint, MiddlePoint, EndPoint };
                    PlayWhooshSound();
                    CurrentPosition[PlaceholderWithFlyingObject][^1].InstanceGameObject.transform.DOPath(Path, LevelCoreMechanicsProviderConfig.DurationOfMovingUp, PathType.CatmullRom).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        CurrentPosition[ID].Add(CurrentPosition[PlaceholderWithFlyingObject][^1]);
                        CurrentPosition[PlaceholderWithFlyingObject].RemoveAt(CurrentPosition[PlaceholderWithFlyingObject].Count - 1);
                        IsMovingNow = true;
                        Vector3 EndPoint = CurrentPosition[ID][^1].InstanceGameObject.transform.position;
                        EndPoint.y = LevelCoreMechanicsProviderConfig.LowestYPositionInGlass + (CurrentPosition[ID].Count - 1) * CurrentPosition[ID][0].Config.Height;
                        PlayWhooshSound();
                        CurrentPosition[ID][^1].InstanceGameObject.transform.DOMove(EndPoint, LevelCoreMechanicsProviderConfig.DurationOfMovingUp).OnComplete(() =>
                        {
                            PlaceholderWithFlyingObject = -1;
                            bool IsVictory = true;
                            for (int i = 0; i < CurrentPosition.Count; i++)
                            {
                                if (CurrentPosition[i].Count == 0)
                                {
                                    continue;
                                }
                                else
                                {
                                    int IndexOfFirstInThisPlaceholder = CurrentPosition[i][0].Config.ID;
                                    for (int j = 1; j < CurrentPosition[i].Count; j++)
                                    {
                                        if (CurrentPosition[i][j].Config.ID != IndexOfFirstInThisPlaceholder)
                                        {
                                            IsVictory = false;
                                            i = CurrentPosition.Count;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (IsVictory)
                            {
                                GameCycleEventHandler.FinishGame();
                            }
                            else
                            {
                                StopMoving();
                            }
                        }
                        );
                    });
                }
            }
            else
            {
                IsMovingNow = true;
                Vector3 EndPoint = CurrentPosition[ID][^1].InstanceGameObject.transform.position;
                EndPoint.y = LevelCoreMechanicsProviderConfig.LowestYPositionInGlass + (CurrentPosition[ID].Count - 1) * CurrentPosition[ID][0].Config.Height;
                PlayWhooshSound();
                CurrentPosition[ID][^1].InstanceGameObject.transform.DOMove(EndPoint, LevelCoreMechanicsProviderConfig.DurationOfMovingUp).OnComplete(() =>
                {
                    PlaceholderWithFlyingObject = -1;
                    StopMoving();
                }
                );
            }
        }
        else
        {
            NextPlaceholder = ID;
        }
    }

    private void PlayWhooshSound()
    {
        AudioPlayer.PlaySound(AudioPlayer.SoundID.WhooshSound);
    }

    private void StopMoving()
    {
        IsMovingNow = false;
        if (NextPlaceholder != -1)
        {
            SelectPlaceholder(NextPlaceholder);
            NextPlaceholder = -1;
        }
    }
}