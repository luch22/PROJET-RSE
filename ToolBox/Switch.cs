using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _WebApp.Infrastructure {
    public class Switch<TElement, TResult> {
        TElement _element;
        TElement _currentCase;
        IDictionary<TElement, TResult> _map = new Dictionary<TElement, TResult>();

        public Switch(TElement element) {
            _element = element;

        }

        //Ajoute un tuple (Case, Then) au dictionnaire
        public Switch<TElement, TResult> Case(TElement element) {
            _currentCase = element;
            return this;
        }

        public Switch<TElement, TResult> Then(TResult result) {
            _map.Add(_currentCase, result);
            return this;
        }

        //Teste tout les éléments du dictionnaire et renvoie default le cas échéant
        public TResult Default(TResult defaultResult) {
            TResult result;
            if (_map.TryGetValue(_element, out result)) {
                return result;
            }
            return defaultResult;
        }
    }
}