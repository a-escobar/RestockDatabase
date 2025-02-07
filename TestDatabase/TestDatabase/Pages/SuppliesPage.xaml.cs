﻿using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TestDatabase.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using SQLiteNetExtensionsAsync.Extensions;



using Xamarin.Forms.Xaml;

namespace TestDatabase
{
     [XamlCompilation(XamlCompilationOptions.Compile)]
     public partial class SuppliesPage : ContentPage
     {
          public SuppliesPage()
          {
               InitializeComponent();
               
          }

          public async void OnNewButtonClicked(object sender, EventArgs args)
          {
               statusMessage.Text = "";
               Provider prov = null;
               Product prod = null;
               
            
               try
               {
                    prod = await App.conn.GetWithChildrenAsync<Product>(productId.Text);

               }
               catch (Exception ex)
               {
                    testMessage.Text = string.Format("Could not Find Product. {0}", ex);
               }

               try
               {
                    prov = await App.conn.GetWithChildrenAsync<Provider>(providerId.Text);

               }
               catch (Exception ex)
               {
                    testMessage.Text = string.Format("Could not Find Provider. {0}", ex);
               }


               try
               {

                    await App.ProductRepo.AddProvider(prod, prov);
               }
               catch (Exception ex){
                    testMessage.Text = string.Format("Could not add provider. {0}", ex);
               }

               //await App.SuppliesRepo.AddNewSupplies(providerId.Text, productId.Text);

               statusMessage.Text = App.ProductRepo.StatusMessage;
               
          }

          public async void OnGetSuppliersButtonClicked(object sender, EventArgs args)
          {
               Product p = await App.conn.GetAsync<Product>(searchProductId.Text);
               List<Provider> providers = await App.ProductRepo.GetAllProviders(p);
               //suppliersList.ItemsSource = providers;
               Console.WriteLine(providers.Count);
               foreach( Provider pr in providers)
               {
                    Console.WriteLine(pr.Name);

               }
          }

          public async void OnSuppliesGetButtonClicked(object sender, EventArgs args)
          {
               statusMessage.Text = "";

               List<Supplies> supplies = await App.SuppliesRepo.GetAllSupplies();
               suppliesList.ItemsSource = supplies;


          }

          public async void OnSearchButtonClicked(object sender, EventArgs args)
          {
               try
               {
                    Product prod = await App.conn.GetAsync<Product>(searchProductId.Text);
                    statusMessage.Text = string.Format("Product Name: {0}", prod.Name);
               }
               catch (Exception ex)
               {
                    statusMessage.Text = string.Format("product Id {0} not found. Error: {1}", searchProductId, ex);
               }
          }

     }
}