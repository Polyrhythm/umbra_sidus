﻿using Hover.Core.Cursors;
using Hover.Core.Items.Types;
using Hover.Core.Renderers;
using Hover.Core.Utils;
using UnityEngine;

namespace Hover.InterfaceModules.Cast {

	/*================================================================================================*/
	[ExecuteInEditMode]
	[RequireComponent(typeof(HovercastInterface))]
	public class HovercastBackCursorTrigger : MonoBehaviour, ITreeUpdateable, ISettingsController {

		public const string CursorTypeName = "CursorType";

		public ISettingsControllerMap Controllers { get; private set; }

		public bool UseFollowedCursorType = true;

		[DisableWhenControlled]
		public CursorType CursorType;

		[Range(0, 1)]
		public float TriggerAgainThreshold = 0.5f;

		private bool vIsTriggered;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		protected HovercastBackCursorTrigger() {
			Controllers = new SettingsControllerMap();
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public void Start() {
			//do nothing...
		}

		/*--------------------------------------------------------------------------------------------*/
		public void TreeUpdate() {
			HovercastInterface cast = gameObject.GetComponent<HovercastInterface>();
			HoverCursorFollower follow = cast.GetComponent<HoverCursorFollower>();

			UpdateCursorType(follow);

			if ( cast.BackItem.IsEnabled ) {
				ICursorData cursorData = follow.CursorDataProvider.GetCursorData(CursorType);
				float triggerStrength = cursorData.TriggerStrength;

				UpdateTrigger(cast, triggerStrength);
				UpdateOverrider(cast.BackItem, triggerStrength);
			}

			Controllers.TryExpireControllers();
		}


		/////////////////////////////////////////////////////////////////////////////////////////////////*--------------------------------------------------------------------------------------------*/
		private void UpdateCursorType(HoverCursorFollower pFollow) {
			if ( UseFollowedCursorType ) {
				Controllers.Set(CursorTypeName, this);
				CursorType = pFollow.CursorType;
			}
		}

		/*--------------------------------------------------------------------------------------------*/
		private void UpdateTrigger(HovercastInterface pCast, float pTriggerStrength) {
			if ( vIsTriggered && pTriggerStrength < TriggerAgainThreshold ) {
				vIsTriggered = false;
				return;
			}

			if ( vIsTriggered || pTriggerStrength < 1 ) {
				return;
			}

			pCast.NavigateBack();
			vIsTriggered = true;
		}
		
		/*--------------------------------------------------------------------------------------------*/
		private void UpdateOverrider(HoverItemDataSelector pBackItem, float pTriggerStrength) {
			HoverRendererIndicatorOverrider rendInd =
				pBackItem.GetComponent<HoverRendererIndicatorOverrider>();

			if ( rendInd == null ) {
				return;
			}

			float minStren = (vIsTriggered ? TriggerAgainThreshold : 0);
			float stren = pTriggerStrength;

			rendInd.Controllers.Set(HoverRendererIndicatorOverrider.MinHightlightProgressName, this);
			rendInd.Controllers.Set(HoverRendererIndicatorOverrider.MinSelectionProgressName, this);

			rendInd.MinHightlightProgress = stren;
			rendInd.MinSelectionProgress = Mathf.InverseLerp(minStren, 1, stren);
		}

	}

}
