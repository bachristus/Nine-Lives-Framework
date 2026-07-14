namespace NineLives.Framework.Unity.UI
{
    public class MenuScreen : AppScreen
    {
        //private MainAppScreen? mainScreen;

        //public override void Initialize(IAppManager gameManager, IUIRequest uiRequest/*, IGameInput input*/)
        //{
        //    base.Initialize(gameManager, uiRequest/*, input*/);

        //    mainScreen = gameObject.GetComponentInParent<MainAppScreen>(includeInactive:true);
        //    if (mainScreen == null) throw new Exception($"'{GetType()}' cannot find parent screen");
        //    mainScreen.Register(this);
        //}        

        //protected virtual void OnDestroy()
        //{
        //    Debug.Log($"OnDestroy() of '{GetType()}' called");
        //    //base.OnDestroy();

        //    mainScreen?.Unregister(this);
        //}

        protected override void OnCancelPressed()
        {
            UIRequest?.RequestToGoBack();            
        }
    }
}