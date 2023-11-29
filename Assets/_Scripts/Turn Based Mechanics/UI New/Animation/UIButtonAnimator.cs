﻿using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace BattleUI {
    public class UIButtonAnimator : UIAnimator {

        [SerializeField] private Transform cursorTarget;
        public Transform CursorTarget => cursorTarget;
        public UIButton Button { get; private set; }

        protected float selectedScale = 1.2f;
        protected float targetScale;
        protected bool selected;

        protected override void Awake() {
            base.Awake();
            Button = GetComponent<UIButton>();
            Button.OnSelect += UIButton_OnSelect;
            Button.OnActivate += UIButton_OnActivate;
        }

        public void OverrideSelect(bool select) => selected = select;

        protected void UIButton_OnSelect() {
            stateAnimator.Brain.UpdateSelection(this);
        }

        protected override IEnumerator Idle() {
            if (selected) {
                transform.DOScale(Vector2.one * selectedScale, animationDuration).SetEase(Ease.OutBounce);
                yield return new WaitForSeconds(animationDuration);
            } else {
                transform.DOScale(Vector2.one * targetScale, animationDuration).SetEase(Ease.InBounce);
                yield return new WaitForSeconds(animationDuration);
            }
        }

        public override void Toggle(bool toggle) {
            base.Toggle(toggle);
            targetScale = toggle ? 1 : 0;
            if (!toggle) selected = false;
        }

        protected void UIButton_OnActivate() {
            transform.DOScale(Vector2.one * 1.3f, 0.1f).SetEase(Ease.OutElastic);
        }
    }
}