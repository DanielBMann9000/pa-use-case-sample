﻿using System;
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

            this.Title = "PreEmptive Analytics Sample";
            

			this.Content = new TableView {
                
				Intent = TableIntent.Menu,
                
				Root = new TableRoot {
					new TableSection ("PreEmptive Analytics Use Case Gallery") {
			                          
			

						new TextCell {
							Text = "About this sample app",
                            TextColor=Xamarin.Forms.Color.White,
							Command = navigateCommand,
							CommandParameter = typeof(AboutPage)
						},
                        
						new TextCell {
							Text = "Capture Feedback & Preferences",
                            TextColor=Xamarin.Forms.Color.White,
							Command = navigateCommand,
							CommandParameter = typeof(FeedbackPage)
						},				

						new TextCell {
							Text = "Measure Performance",
                            TextColor=Xamarin.Forms.Color.White,
							Command = navigateCommand,
							CommandParameter = typeof(PerformancePage)
						},
                        		new TextCell {
							Text = "Monitor Exceptions",
                            TextColor=Xamarin.Forms.Color.White,
							Command = navigateCommand,
							CommandParameter = typeof(ExceptionPage)
						},

						new TextCell {
							Text = "Show Identity",
                            TextColor=Xamarin.Forms.Color.White,
							Command = navigateCommand,
							CommandParameter = typeof(IdentityPage)
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