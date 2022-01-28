using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StTask.Model
{
        public partial class Users
        {
            public long Page { get; set; }
            public long PerPage { get; set; }
            public long Total { get; set; }
            public long TotalPages { get; set; }
            public ObservableCollection<User> Data { get; set; }
            public Support Support { get; set; }
        }

        public partial class User
        {
            public long Id { get; set; }
            public string Email { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public Uri Avatar { get; set; }
        }

        public partial class Support
        {
            public Uri Url { get; set; }
            public string Text { get; set; }
        }
    

}
