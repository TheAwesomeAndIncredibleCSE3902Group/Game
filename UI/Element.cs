// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI;

public class Element
{
    public Rectangle OffsetAndSize { get; set; } = new Rectangle();
    private Point _derivedAbsolutePositionBase;
    public Point DerivedAbsolutePosition
    {
        get
        {
            return _derivedAbsolutePositionBase + OffsetAndSize.Location;
        }
    }
    public bool DerivedAncestorIsSelected { get; private set; } = false;
    public bool DerivedAncestorIsVisible { get; private set; } = true;
    protected internal List<Element> _children = [];
    public bool IsSelectable { get; private set; } = false;
    public bool IsSelected { get; set; } = false;
    public bool IsVisible { get; set; } = true;
    public float Opacity { get; set; } = 1f;
    public RootElement RootElement { get; protected set; }
    public Element Parent { get; private set; }
    private readonly Dictionary<UIEvent, List<Action<UIEventParamsBase> >> _registeredUiEventActions = [];
    public Dictionary<string, object> Attributes { get; } = [];
    
    // For debugging stuffs!!!!!!!
    #pragma warning disable IDE0052
    private readonly string DEBUG_codeThatCreatedMe;

    // Constructor that should ONLY be used by RootElement
    protected Element()
    {
        SetUpElement(null);
    }

    public Element(RootElement rootElement, [CallerFilePath] string debugCallerFilePath = "", [CallerLineNumber] int callerLineNumber = -1)
    {
        SetUpElement(rootElement);

        // This is a very cool C# feature!
        DEBUG_codeThatCreatedMe = debugCallerFilePath + " (" + callerLineNumber + ")";
    }

    protected internal void Draw(GameTime gameTime)
    {
        DispatchUIEvent(UIEvent.Draw, new DrawUIEventParams(this, gameTime));
    }

    protected internal void Update(GameTime gameTime)
    {
        DispatchUIEvent(UIEvent.Update, new DrawUIEventParams(this, gameTime));
    }

    public void AddChild(Element element)
    {
        if (element.Parent == null)
        {
            element.SetUpAsChild(this);
            _children.Add(element);
        } else
        {
            Console.Error.WriteLine("Child already has parent!");
        }
    }

    public void RemoveChild(Element element)
    {
        if (element.Parent == this)
        {
            element.Parent = null;
            _children.Remove(element);
        }
        else
        {
            Console.Error.WriteLine("Child does not have this element as a parent!");
        }
    }

    // make shallow clone of list, so anything done with this list
    // doesn't affect things in here
    public List<Element> GetChildren()
    {
        return new(_children);
    }

    protected internal void CalculateDerivedValuesFromAncestors()
    {
        if (Parent != null)
        {
            _derivedAbsolutePositionBase = Parent.DerivedAbsolutePosition;
            DerivedAncestorIsSelected = Parent.IsSelected || Parent.DerivedAncestorIsSelected;
            DerivedAncestorIsVisible = Parent.IsVisible && Parent.DerivedAncestorIsVisible;
        }
    }

    public void MakeSelectable()
    {
        if (!IsSelectable)
        {
            IsSelectable = true;
            RootElement.UIState.RegisterSelectableElement(this);
        }
    }

    public void MakeUnselectable()
    {
        IsSelectable = false;
        RootElement.UIState.UnregisterSelectableElement(this);
    }

    protected void SetUpElement(RootElement rootElement)
    {
        RootElement = rootElement;
        foreach (UIEvent uiEventType in Enum.GetValues<UIEvent>())
        {
            _registeredUiEventActions[uiEventType] = [];
        }
    }
    
    protected void SetUpAsChild(Element parentElement)
    {
        this.Parent = parentElement;
    }

    public void AddActionOnUIEvent(UIEvent uiEvent, Action<UIEventParamsBase> action)
    {
        _registeredUiEventActions[uiEvent].Add(action);
    }
    public void RemoveActionOnUIEvent(UIEvent uiEvent, Action<UIEventParamsBase> action)
    {
        _registeredUiEventActions[uiEvent].Remove(action);
    }
    public void DispatchUIEvent(UIEvent uiEvent, UIEventParamsBase uiEventInfo)
    {
        foreach (Action<UIEventParamsBase> uiAction in _registeredUiEventActions[uiEvent])
        {
            uiAction(uiEventInfo);
        }
    }
}