﻿namespace MauiAppScanning.ViewModels;

[QueryProperty(nameof(Item), "Item")]
public partial class ListDetailDetailViewModel : BaseViewModel
{
	[ObservableProperty]
	SampleItem item;
}
