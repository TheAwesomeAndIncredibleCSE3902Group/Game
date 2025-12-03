// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using System;
using System.Collections.Generic;
using AwesomeRPG.Sprites;
using AwesomeRPG.UI.Components;
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
    private bool _isBeingUsedAsComponentRoot = false;
    protected internal readonly Dictionary<UIEvent, List<Action<UIEventParamsBase> >> _registeredUiEventActions = [];

    protected internal virtual void Draw(GameTime gameTime)
    {
        // Do nothing by default
        System.Console.WriteLine("DRAWING DEFAULT FOR:" + this);
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
        if (_isBeingUsedAsComponentRoot)
        {
            _derivedAbsolutePositionBase = Parent.Parent.DerivedAbsolutePosition;
            DerivedAncestorIsSelected = Parent.Parent.IsSelected || Parent.Parent.DerivedAncestorIsSelected;
            DerivedAncestorIsVisible = Parent.Parent.IsVisible && Parent.Parent.DerivedAncestorIsVisible;
        } else if (Parent != null)
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
        System.Console.WriteLine(this + " is used as component root? " + this._isBeingUsedAsComponentRoot);
        foreach (Action<UIEventParamsBase> uiAction in _registeredUiEventActions[uiEvent])
        {
            uiAction(uiEventInfo);
        }
    }

    protected internal void UseAsComponentRoot(ComponentBase component)
    {
        if (_isBeingUsedAsComponentRoot == true)
        {
            throw new Exception("Attempted to use element as component root when it is already being used as a component root.");
        }
        if (this.Parent != null)
        {
            throw new Exception("Cannot use element as compoment root if it has a parent element.");
        }
        this._isBeingUsedAsComponentRoot = true;
        this.Parent = component;
    }

    protected internal void UndoUseAsComponentRoot(ComponentBase component)
    {
        if (_isBeingUsedAsComponentRoot == false)
        {
            throw new Exception("Attempted to undo element being used as component root when it already isn't being used as one.");
        }
        this.Parent = null;
    }
}