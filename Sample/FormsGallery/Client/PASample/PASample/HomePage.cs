using System;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;

namespace PASample
{
    class HomePage : BasePage
    {
        public HomePage()
		{
			// Define command for the items in the TableView.
			Command<Type> navigateCommand = 
				new Command<Type> (async (Type pageType) => {
					// Get all the constructors of the page type.
					IEnumerable<ConstructorInfo> constructors = 
						pageType.GetTypeInfo ().DeclaredConstructors;

					foreach (ConstructorInfo constructor in constructors) {
						// Check if the constructor has no parameters.
						if (constructor.GetParameters ().Length == 0) {
							// If so, instantiate it, and navigate to it.
							Page page = (Page)constructor.Invoke (null);
							await this.Navigation.PushAsync (page);
							break;
						}
					}
				});

			this.Title = "PA Sample App";
            

			this.Content = new TableView {
                
				Intent = TableIntent.Menu,
                
				Root = new TableRoot {
					new TableSection ("PreEmptive Analytics Use Case Gallery") {
			                          
			

						new TextCell {
							Text = "About",
                            TextColor=Xamarin.Forms.Color.White,
							Command = navigateCommand,
							CommandParameter = typeof(AboutPage)
						},

						new TextCell {
							Text = "Identity",
                            TextColor=Xamarin.Forms.Color.White,
							Command = navigateCommand,
							CommandParameter = typeof(IdentityPage)
						},

						new TextCell {
							Text = "Exception",
                            TextColor=Xamarin.Forms.Color.White,
							Command = navigateCommand,
							CommandParameter = typeof(ExceptionPage)
						},

						new TextCell {
							Text = "Performance",
                            TextColor=Xamarin.Forms.Color.White,
							Command = navigateCommand,
							CommandParameter = typeof(PerformancePage)
						},

						new TextCell {
							Text = "Feedback & Preferences",
                            TextColor=Xamarin.Forms.Color.White,
							Command = navigateCommand,
							CommandParameter = typeof(FeedbackPage)
						}
					}
				}
			};


		}

        public override string Feature
        {
            get
            {
                return "Home";
            }
        }
    }
}
