// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Sets the lock mode of the mouse cursor.")]
	public class SetCursorMode : FsmStateAction
	{
		[Tooltip("The texture to use for the cursor or null to set the default cursor. \n\n" +
			"Note that a texture needs to be imported with 'Read/Write enabled' in the texture importer (or using the 'Cursor' defaults), in order to be used as a cursor.")]
		[ObjectType(typeof(Texture2D))]
		public FsmObject cursorTexture;

		[Tooltip("The offset from the top left of the texture to use as the target point (must be within the bounds of the cursor). \n\n" + "0,0 is normal behavior.")]
		public FsmVector2 hotSpot;

		public enum RenderMode
		{
			Auto,
			ForceSoftware
		}

		public enum CurState
		{
			None,
			LockedToCenter,
			ConfinedToGameWindow
		}

	    public enum UpdateType
	    {
            OnGui,
            Once,
	        Update,
            LateUpdate

	    }

		[Tooltip("\nAuto: Use hardware cursors on supported platforms.\n\n" +
			"Or\n\nForce the use of software cursors.")]
		public RenderMode renderMode;

		[Tooltip("\nFree Movement\nLocked to window center\nFree, but Confined to the game window")]
		public CurState lockMode;

	    public UpdateType updateType;

		[Tooltip("Hide the cursor?")]
		public FsmBool hideCursor;

		private CursorMode _renderAs;
		private CursorLockMode _newMode;

		public override void Reset()
		{
			cursorTexture = null;
			hotSpot = new FsmVector2 { UseVariable = true };

			renderMode = RenderMode.Auto;
			lockMode = CurState.None;
			hideCursor = true;
		}

	    public override void OnEnter() 
        {
	        ApplyMode();
            if (updateType == UpdateType.Once) Finish();
	    }

	    public override void OnUpdate() { if (updateType == UpdateType.Update) ApplyMode(); }
	    public override void OnLateUpdate() { if (updateType == UpdateType.LateUpdate) ApplyMode(); }
        public override void OnGUI() { if (updateType == UpdateType.OnGui) ApplyMode(); }

	    public void ApplyMode()
	    {
            switch (lockMode) 
			{
			case CurState.None:
				_newMode = CursorLockMode.None;
				break;
			case CurState.LockedToCenter:
				_newMode = CursorLockMode.Locked;
				break;
			case CurState.ConfinedToGameWindow:
				_newMode = CursorLockMode.Confined;
				break;
			}

			switch (renderMode) 
			{
			case RenderMode.Auto:
				_renderAs = CursorMode.Auto;
				break;
			case RenderMode.ForceSoftware:
				_renderAs = CursorMode.ForceSoftware;
				break;
			}

			Cursor.visible = !hideCursor.Value;

            Texture2D newTexture = cursorTexture.Value as Texture2D;
		    Cursor.SetCursor(newTexture, (hotSpot.IsNone) 
                ? Vector2.zero
                : hotSpot.Value, _renderAs);

			Cursor.lockState = _newMode;
	    }
	}
}