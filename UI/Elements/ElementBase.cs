// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

// This is the root element that will contain every other class. This should only be used once.

using System;
using System.Collections.Generic;
using AwesomeRPG.Sprites;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI.Elements;

public abstract class ElementBase
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
    private List<ElementBase> _children = [];
    public bool IsSelectable { get; private set; } = false;
    public bool IsSelected = false;
    public bool IsVisible = true;
    public RootElement RootElement { get; protected set; }
    public ElementBase Parent { get; private set; }
    private readonly Dictionary<UIEvent, List<Action<UIEventBase> >> _registeredUiEventActions = [];

    public abstract void Draw(GameTime gameTime);

    public void AddChild(ElementBase element)
    {
        if (element.Parent == null)
        {
            element.Parent = this;
            _children.Add(element);
        } else
        {
            Console.Error.WriteLine("Child already has parent!");
        }
    }

    public void RemoveChild(ElementBase element)
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
    public List<ElementBase> GetChildren()
    {
        return new(_children);
    }

    protected void CalculateDerivedValuesFromAncestors()
    {
        _derivedAbsolutePositionBase = Parent.DerivedAbsolutePosition;
        DerivedAncestorIsSelected = Parent.IsSelected || Parent.DerivedAncestorIsSelected;
    }

    protected void RunBeforeDrawActions(GameTime gameTime)
    {
        // System.Console.WriteLine(_registeredUiEventActions.ToString());
        foreach (Action<UIEventBase> uiAction in _registeredUiEventActions[UIEvent.BeforeDraw])
        {
            uiAction(new DrawUIEvent(this, gameTime));
        }
    }

    protected void RunAfterDrawActions(GameTime gameTime)
    {
        foreach (Action<UIEventBase> uiAction in _registeredUiEventActions[UIEvent.AfterDraw])
        {
            uiAction(new DrawUIEvent(this, gameTime));
        }
    }

    protected void DrawChildren(GameTime gameTime)
    {
        // We will draw each of this element's children.
        foreach (ElementBase child in _children)
        {
            child.Draw(gameTime);
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
            // System.Console.WriteLine(uiEventType);
        }
    }

    public void AddActionOnUIEvent(UIEvent uiEvent, Action<UIEventBase> action)
    {
        _registeredUiEventActions[uiEvent].Add(action);
    }
    public void RemoveActionOnUIEvent(UIEvent uiEvent, Action<UIEventBase> action)
    {
        _registeredUiEventActions[uiEvent].Remove(action);
    }
    public void DispatchUIEvent(UIEvent uiEvent, UIEventBase uiEventInfo)
    {
        foreach (Action<UIEventBase> uiAction in _registeredUiEventActions[uiEvent])
        {
            uiAction(uiEventInfo);
        }
    }
}