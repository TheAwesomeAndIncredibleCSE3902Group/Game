// Originally written by Eli L
// If you need help with working on UI, feel free to ask me!!!

using System;
using System.Collections.Generic;
using AwesomeRPG.UI.Events;

namespace AwesomeRPG.UI.Elements;

public class UIState
{
    private readonly List<ElementBase> _selectableElements = [];
    private int _selectionIndex = 0; // This is the "location" of the currently selected element. Can be null
    public int SelectionIndex
    {
        get
        {
            return _selectionIndex;
        }
        set
        {
            SelectedElement.DispatchUIEvent(UIEvent.Unselect, new PlainUIEventParams(SelectedElement));
            _selectionIndex = ((value % _selectableElements.Count) + _selectableElements.Count) % _selectableElements.Count;
            SelectedElement.DispatchUIEvent(UIEvent.Select, new PlainUIEventParams(SelectedElement));
        }
    }
    // private Dictionary<(UIControl, UIControlEvent), List<Action>> UIControlActions = [];
    private RootElement _rootElement;
    public ElementBase SelectedElement
    {
        get
        {
            if (_selectionIndex >= 0 || _selectionIndex < _selectableElements.Count)
            {
                return _selectableElements[_selectionIndex];
            }
            else
            {
                // Element not in list
                return null;
            }
        }
        set
        {
            int selectionIdx = _selectableElements.IndexOf(value);
            if (selectionIdx == -1)
            {
                Console.Error.WriteLine("WARNING: Selected an element that is not registered as a selectable element!");
            }
            _selectionIndex = selectionIdx;
        }
    }

    public void RegisterSelectableElement(ElementBase element)
    {
        if (element.RootElement != _rootElement)
        {
            Console.Error.WriteLine("Attempted to register element that is not associated with the same root node.");
            return;
        }
        if (!_selectableElements.Contains(element))
        {
            _selectableElements.Add(element);
        }
        else
        {
            Console.Error.WriteLine("Attempted to register element that was already registered");
        }
    }

    public void UnregisterSelectableElement(ElementBase element)
    {
        bool removed = _selectableElements.Remove(element);
        if (!removed)
        {
            Console.Error.WriteLine("Attempted to unregister element that was not registered");
        }
    }
    public UIState(RootElement rootElement)
    {
        _rootElement = rootElement;
    }

    public void UpdateElementsAreSelected()
    {
        int idx = 0;
        foreach (ElementBase element in _selectableElements)
        {
            element.IsSelected = _selectionIndex == idx;
            idx++;
        }
    }
}
