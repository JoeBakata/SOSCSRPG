   M S B A M L   `   `   `  ����A  =Engine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE/clr-namespace:Engine.ViewModels;assembly=EngineEngine.ViewModels  5   	   	 WPFUI   WPFUI.TradeScreen   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   F
viewModels/clr-namespace:Engine.ViewModels;assembly=Engine     5   	     � WindowStartupLocation$  CenterOwner=�5	   	   $
��11pt(�5
   	    � Title$ Trade Screen��5   	   $	��480��6   $	��640��6+   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��2��5      $��Center=�5      ��5      �� *CurrentTrader.Name+�� 5"   
   $��1��6   $��0��6   $��Center=�5#      $��Your Inventory��5$      �� 5'   
   $��1��6   $��1��6   $��Center=�5(      $��Trader's Inventory��5)      &   System.Windows.Controls.DataGrid  5/   
   $��2��6   $��0��6    $��10q�50         AutoGenerateColumns .     HeadersVisibility$ Column=�53         CanUserAddRows .     CanUserDeleteRows .  "   VerticalScrollBarVisibility$
 Auto=�56      ��51      �� * CurrentPlayer.GroupedInventory+	   Columns	 58      0  *System.Windows.Controls.DataGridTextColumn  59      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Description��6%      
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5;   %   1  +System.Windows.Controls.DataGridBoundColumn   Binding 5<   %   �� *	Item.Name+  5=      $	
 Qty��6%    .  $
 Auto 5?   %    5@   %   �� *
Quantity+  5A      $
 Price��6%    . $
 Auto 5C   %    5D   %   �� *
Item.Price+4  .System.Windows.Controls.DataGridTemplateColumn  5E         MinWidth$ 75Q�6)      CellTemplate 5F      �� 5G      .R�5H      �� -   5        $��55��5I   %   $��Sell 1��5J   %   5K      5M      5P        5S   
   $��2��6   $��1��6    $��10q�5T       .  $ Column=�5W       .   .  $
 Auto=�5Z      ��5U      �� * CurrentTrader.GroupedInventory+	 5\        5]      $
 Description��6%    . $ * 5_   %    5`   %   �� *	Item.Name+  5a      $	
 Qty��6%    .  $
 Auto 5c   %    5d   %   �� *
Quantity+  5e      $
 Price��6%    . $
 Auto 5g   %    5h   %   �� *
Item.Price+  5i      $ 75Q�6)    5j      �� 5k      .R�5l      �� -   5        $��55��5m   %   $��Buy 1��5n   %   5o      5q      5s      �� 5v   
   -   5        $��3��5v      $��1��6   $��Right=�5w      $��75��5x      $��Close��5y      5z      5{      5|      