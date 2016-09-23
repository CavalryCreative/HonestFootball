
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using HonestFootball.ViewModels;
using HonestFootball.Models;
using System;

namespace HonestFootball.Droid.Activities
{
    [Activity(Label = "Comments")]
    public class CommentsActivity : BaseActivity<CommentsViewModel>
    {
        ListView listView;
        Adapter adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Comments);

            listView = FindViewById<ListView>(Resource.Id.conversationsList);

            listView.Adapter =
                adapter = new Adapter(this);

            listView.ItemClick += (sender, e) =>
            {
                viewModel.Comment = adapter[e.Position];

                //StartActivity(typeof(MessagesActivity));
            };
        }

        protected async override void OnResume()
        {
            base.OnResume();

            try
            {
                await viewModel.GetComments();

                adapter.NotifyDataSetInvalidated();
            }
            catch (Exception exc)
            {
                DisplayError(exc);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //MenuInflater.Inflate(Resource.Menu.ConversationsMenu, menu);
            //return base.OnCreateOptionsMenu(menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //if (item.ItemId == Resource.Id.addFriendMenu)
            //{
            //    StartActivity(typeof(FriendsActivity));
            //}
            //return base.OnOptionsItemSelected(item);

            return true;
        }
    }

    class Adapter : BaseAdapter<Comment>
    {
        readonly CommentsViewModel commentsViewModel = ServiceContainer.Resolve<CommentsViewModel>();
        readonly LayoutInflater inflater;

        public Adapter(Context context)
        {
            inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
        }

        public override long GetItemId(int position)
        {
            return commentsViewModel.Comments[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = inflater.Inflate(Resource.Layout.Comments, null);
            }

            var comment = this[position];
            var username = convertView.FindViewById<TextView>(Resource.Id.conversationUsername);
            var lastComment = convertView.FindViewById<TextView>(Resource.Id);

            lastComment.Text = comment.Text;

            return convertView;
        }

        public override int Count
        {
            get { return commentsViewModel.Comments == null ? 0 : commentsViewModel.Comments.Length; }
        }

        public override Comment this[int index]
        {
            get { return commentsViewModel.Comments[index]; }
        }
    }
}