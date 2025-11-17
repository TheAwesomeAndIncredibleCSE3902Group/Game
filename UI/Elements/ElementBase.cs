// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using System;
using System.Collections.Generic;
using AwesomeRPG.Sprites;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI.Elements;

public abstract class ElementBase
{
    public virtual Rectangle OffsetAndSize { get; set; } = new Rectangle();
    protected Point _derivedAbsolutePositionBase = Point.Zero;
    public Point DerivedAbsolutePosition
    {
        get
        {
            return _derivedAbsolutePositionBase + OffsetAndSize.Location;
        }
    }
    public bool DerivedAncestorIsSelected { get; private set; } = false;
    public bool DerivedAncestorIsVisible { get; private set; } = true;
    protected internal List<ElementBase> _children = [];
    public bool IsSelectable { get; private set; } = false;
    public bool IsSelected { get; set; } = false;
    public bool IsVisible { get; set; } = true;
    public float Opacity { get; set; } = 1f;
    public RootElement RootElement { get; protected set; }
    public ElementBase Parent { get; private set; }
    protected internal readonly Dictionary<UIEvent, List<Action<UIEventParamsBase> >> _registeredUiEventActions = [];

    protected internal virtual void Draw(GameTime gameTime)
    {
        // Do nothing by default
        System.Console.WriteLine("DRAWING DEFAULT?");
    }

    protected internal virtual void Update(GameTime gameTime)
    {
        // Do nothing by default
    }

    public void AddChild(ElementBase element)
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

    // This will remove the element from its parent.
    public void Remove()
    {
        Parent._children.Remove(this);
    }

    // make shallow clone of list, so anything done with this list
    // doesn't affect things in here
    public List<ElementBase> GetChildren()
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

    protected void RunBeforeDrawActions(GameTime gameTime)
    {
        // System.Console.WriteLine(_registeredUiEventActions.ToString());
        foreach (Action<UIEventParamsBase> uiAction in _registeredUiEventActions[UIEvent.BeforeDraw])
        {
            uiAction(new DrawUIEventParams(this, gameTime));
        }
    }

    protected void RunAfterDrawActions(GameTime gameTime)
    {
        foreach (Action<UIEventParamsBase> uiAction in _registeredUiEventActions[UIEvent.AfterDraw])
        {
            uiAction(new DrawUIEventParams(this, gameTime));
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

    // deprecated
    protected void SetUpElement(RootElement rootElement)
    {
        RootElement = rootElement;
        foreach (UIEvent uiEventType in Enum.GetValues<UIEvent>())
        {
            _registeredUiEventActions[uiEventType] = [];
        }
    }
    
    protected void SetUpAsChild(ElementBase parentElement)
    {
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