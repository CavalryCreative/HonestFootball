using System;

using Android.App;
using Android.Content;
using Android.OS;

using HonestFootball.ViewModels;

namespace HonestFootball.Droid.Activities
{
    [Activity]
    public class BaseActivity<TViewModel> : Activity where TViewModel : BaseViewModel
    {
        protected readonly TViewModel viewModel;
        protected ProgressDialog progress;

        public BaseActivity()
        {
            viewModel = ServiceContainer.Resolve(typeof(TViewModel)) as TViewModel;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            progress = new ProgressDialog(this);
            progress.SetCancelable(false);
            progress.SetTitle(Resource.String.Loading);
        }

        protected override void OnResume()
        {
            base.OnResume();
            viewModel.IsBusyChanged += OnIsBusyChanged;
        }

        protected override void OnPause()
        {
            base.OnPause();
            viewModel.IsBusyChanged -= OnIsBusyChanged;
        }

        void OnIsBusyChanged(object sender, EventArgs e)
        {
            if (viewModel.IsBusy)
                progress.Show();
            else
                progress.Hide();
        }

        protected void DisplayError(Exception exc)
        {
            string error = exc.Message;

            new AlertDialog.Builder(this)
            .SetTitle(Resource.String.ErrorTitle)
            .SetMessage(error)
            .SetPositiveButton(Android.Resource.String.Ok, (IDialogInterfaceOnClickListener)null)
            .Show();
        }

        protected void DisplayText(string txt)
        {
            new AlertDialog.Builder(this)
            .SetTitle(Resource.String.ErrorTitle)
            .SetMessage(txt)
            .SetPositiveButton(Android.Resource.String.Ok, (IDialogInterfaceOnClickListener)null)
            .Show();
        }
    }
}