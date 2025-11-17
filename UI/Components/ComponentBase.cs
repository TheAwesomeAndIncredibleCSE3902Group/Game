using System;
using System.Collections.Generic;
using AwesomeRPG.UI.Elements;
using AwesomeRPG.UI.Events;
using Microsoft.Xna.Framework;

namespace AwesomeRPG.UI.Components;


public abstract class ComponentBase : ElementBase
{
    private ElementBase _componentBaseElement;
    public ElementBase ComponentBaseElement {
        get
        {
            return _componentBaseElement;
        } 
        protected set
        {
            _componentBaseElement = value;
        }
    }

    public override Rectangle OffsetAndSize {
        get
        {
            return ComponentBaseElement.OffsetAndSize;
        }
        set
        {
            ComponentBaseElement.OffsetAndSize = value;
        }
    }
    
    protected internal override void Draw(GameTime gameTime)
    {
        ComponentBaseElement.CalculateDerivedValuesFromAncestors();
        ComponentBaseElement.Draw(gameTime);
    }
    protected internal override void Update(GameTime gameTime)
    {
        ComponentBaseElement.CalculateDerivedValuesFromAncestors();
        ComponentBaseElement.Update(gameTime);
    }
    public new void AddActionOnUIEvent(UIEvent uiEvent, Action<UIEventParamsBase> action)
    {
        ComponentBaseElement._registeredUiEventActions[uiEvent].Add(action);
    }
    public new void RemoveActionOnUIEvent(UIEvent uiEvent, Action<UIEventParamsBase> action)
    {
        ComponentBaseElement._registeredUiEventActions[uiEvent].Remove(action);
    }
    public new void DispatchUIEvent(UIEvent uiEvent, UIEventParamsBase uiEventInfo)
    {
        foreach (Action<UIEventParamsBase> uiAction in ComponentBaseElement._registeredUiEventActions[uiEvent])
        {
            uiAction(uiEventInfo);
        }
    }
}