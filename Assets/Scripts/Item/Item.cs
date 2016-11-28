using UnityEngine;
using System.Collections;
using Assets.Scripts.Util;

public class Item : MonoBehaviour
{
    public enum ItemType { Get, Interact, StockWoodMaker, Picture };
    public ItemType currType;

    bool destroyed;
    public bool hide = false;

    [SerializeField]
    string codItem;

    [SerializeField]
    string sub;

    [SerializeField]
    string subRelatedItem;

    [SerializeField]
    string relatedItemCod;

    [SerializeField]
    string eventItem;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Hide()
    {
        MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();

        destroyed = true;
        hide = true;

        if (mesh != null)
        {
            mesh.enabled = false;
        }
        else
        {
            SetVisibility(false);
        }
    }

    public void Show()
    {
        MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();

        destroyed = false;

        if (mesh != null)
        {
            mesh.enabled = true;
        }
        else
        {
            SetVisibility(true);
        }
    }

    public void Destroy()
    {
        if (!destroyed)
        {
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();

            switch (currType)
            {
                case ItemType.Get:
                    SubtitleManager.GetInstance().SetText(sub);

                    if (!string.IsNullOrEmpty(eventItem))
                    {
                        EventManager.GetInstance().SetEvent(eventItem);
                    }

                    destroyed = true;
                    hide = false;

                    audio.Play();

                    if (mesh != null)
                    {
                        mesh.enabled = false;
                    }
                    else
                    {
                        SetVisibility(false);
                    }

                    break;
                case ItemType.Interact:
                    if (!Player.GetInstance().Items.Contains(relatedItemCod))
                    {
                        SubtitleManager.GetInstance().SetText(subRelatedItem);
                    }
                    else
                    {
                        SubtitleManager.GetInstance().SetText(sub);
                        destroyed = true;
                        audio.Play();
                    }
                    break;

                case ItemType.StockWoodMaker:
                    if (!Player.GetInstance().Items.Contains(Constants.StockWood))
                    {
                        SubtitleManager.GetInstance().SetText("Pegou Lenha");
                        Player.GetInstance().Items.Add(Constants.StockWood);
                    }
                    else
                    {
                        SubtitleManager.GetInstance().SetText("Você já tem lenha");
                    }
                    break;
            }
        }
    }

    void SetVisibility(bool visibility)
    {
        Renderer[] rs = GetComponentsInChildren<Renderer>();

        foreach (Renderer r in rs)
        {
            r.enabled = visibility;
        }
    }

    public string CodItem
    {
        get { return codItem; }
    }

    public bool isDestroyed
    {
        get { return destroyed; }
    }
}
