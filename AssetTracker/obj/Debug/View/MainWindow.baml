   M S B A M L   `   `   `  ����G  CAssetTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:AssetTrackerAssetTracker  5   	   5clr-namespace:AssetTracker.ViewAssetTracker.View  5   	   A%clr-namespace:AssetTracker.ViewModelsAssetTracker.ViewModels  5   	   H DMaterialIcons, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:MaterialIcons;assembly=MaterialIconsMaterialIcons 5	   	   K*clr-namespace:AssetTracker.View.ConvertersAssetTracker.View.Converters  5
   	       AssetTracker.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:AssetTracker     5   	   0Viewclr-namespace:AssetTracker.View     5   	   ;	viewmodel%clr-namespace:AssetTracker.ViewModels     5   	   Lmaterialicons2clr-namespace:MaterialIcons;assembly=MaterialIcons     5	   	   A
converters*clr-namespace:AssetTracker.View.Converters     5
   	     � Title$  
MainWindow��5   	   $
��1080��5   	   $
��1920��5   	    � WindowState$ 	Maximized=�5   	   .��5      �    BackgroundColor_Dark#��[  6    � 	Resources 5   
     NavText �� 
TargetType �� Property �� Value��%�   &	       �� 5      " ��5      .�5      ��  � ��$ 14(�5      �� 5       � ��$ 10, 0q�5      �� 5       � ��$ Center=�5      �� 5       � ��$ 10q�5      5      5       � RowDefinitions 5!   
   �� 5"      $
��60px �6   �� 5#      .+�5&   
   � -   5         �Name$ NavBar��5&      $��0��5'      $��5q�5(      y�5)      �� *CurrentUser+  IsNullInvertToVizConverter �� 	Converter# [ .+�5+      � $
��20,0q�6   .+�5,      �� -   5        $��75��5-      $��0��5.      $
��Left=�5/      .��50         MaterialIcons.MaterialIcon  	   Icon$	 ic_place=�61     	Text_Dark#��[ 51   1   52      �� 54      -   5        
 ��Name$	
 Nav��54       �� Orientation$ 
Horizontal=�55      $
��50,0q�56      $��Center=�57      $��Center=�58      .+�5:      �� -   5          HoverBtn#��[ 5:   9   .��5;        $	 ic_list=�65     HoverBtn_Content#��[ 5<   5   5=      �� 5?      -   5        {�.  $
��50,0q�5A      #��[ 6+   .��5B        $	 ic_equalizer=�65   #��[ 6I   5C      �� 5E      -   5        #��[ 5E   =   .��5F        $	 ic_settings=�65   #��[ 6H   5G      5I      �� 5K      $ 
Horizontal=�6   $��Center=�5L      $��Right=�5M      .+�5O      �� $��14(�6    $��Center=�5P       $��Center=�5Q       $
��10,0q�5R       
��������� 5T      �6   �� *CurrentUser.us_fname+ �� 5U      �6   �� *CurrentUser.us_lname+5V      �� 5X      -   5         ��Name$ 
ProfileBtn��5X      
������ .��5[        $	 	ic_person=�65   $��25��5\   5   $��25��5]   5   
�������5^   5   5`      5d      5e      5f       � 5j   
   -   5          �Name$ ContentFrame��5j      $��1��5k        � NavigationUIVisibility$ Hidden=�5l      5n      � 5p   
   -	   5         �Name$ ProfileBtn_Popup��5p       � 	StaysOpen .  $��2��5s       � HorizontalOffset$
 -170��5t       � VerticalOffset$ 10��6(    � PlacementTarget 5q      ��  �� ElementName$ 
ProfileBtn��.��5u      ��   Layer2_Dark#��[ 6   $	��200��6?    �� Padding$ 20,10q�6K   $
��Left=�6[    5v        HoverBtn_TextContent �� BasedOn��%�   '��       7  �� 5w      8   " ��5x      .�5y      ��  � ��$ Right=�5z   !   �� 5{       � ��$ 16(�5|   !   5}      5~      ._�5      �� .+�5�      �� -
   5        #��[ 5�      .��5�      �� $��Profile Settings��6$   5�      �� 5�      -   5        #��[ 5�      .��5�      �� $��Logout��6$   5�      5�      5�      5�      5�      5�      