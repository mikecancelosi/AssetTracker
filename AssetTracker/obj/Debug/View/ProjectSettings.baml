   M S B A M L   `   `   `  ����G  CAssetTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:AssetTracker.ViewAssetTracker.View  5      A%clr-namespace:AssetTracker.ViewModelsAssetTracker.ViewModels  5      H DMaterialIcons, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:MaterialIcons;assembly=MaterialIconsMaterialIcons 5      K*clr-namespace:AssetTracker.View.ConvertersAssetTracker.View.Converters  5	      G(clr-namespace:AssetTracker.View.ControlsAssetTracker.View.Controls  5
      '    !AssetTracker.View.ProjectSettings   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:AssetTracker.View     5      ;	viewmodel%clr-namespace:AssetTracker.ViewModels     5      Lmaterialicons2clr-namespace:MaterialIcons;assembly=MaterialIcons     5      ;conv*clr-namespace:AssetTracker.View.Converters     5	      =controls(clr-namespace:AssetTracker.View.Controls     5
      -   5          N�Name$  ProjectSettingsPage��5       N� Title$ ProjectSettings��5       N� ShowsNavigationUI .  .��5      � $��100,0q�6    � ColumnDefinitions 5   
   �� 5      $��2* �6   $	��300��5      �� 5      $��5* �6   .+�5   
   �� -   5         ��Name$ 	LeftPanel��5      $��0��5         	LeftPanel#��[  5      ._�5      � .+�5      �� $��Settings��6     LeftPanel_Header#��[ 5        	Text_Dark#��[ 5      �� 5      $��0,100q�6   .+�5       2   ,AssetTracker.View.Controls.SubNavRadioButton  -   5          Name$ UsersBtn��5    1      
IconChoice$ 	ic_person=�5!   1      LabelContent$ Users��5"   1   $��0,0,0,20q�5#   1    	� 	GroupName$ 
Navigation��5$   1   	 d� 	IsChecked$
	 TrueY�5%   1     5&      -   5        $ RolesBtn��5&   1   $ ic_group=�5'   1   $ Roles��5(   1   $��0,0,0,20q�5)   1   $ 
Navigation��5*   1     5+      -   5        $ CategoriesBtn��5+   1   $ ic_group_work=�5,   1   $ 
Categories��5-   1   $��0,0,0,20q�5.   1   $ 
Navigation��5/   1     50      -   5        $ 	AlertsBtn��50   1   $ ic_priority_high=�51   1   $ Alerts��52   1   $��0,0,0,20q�53   1   $ 
Navigation��54   1   55      56      57      �� 59   
   -   5        $ 
RightPanel��59      $��1��5:        
RightPanel#��[ 5;      ._�5<      � 
 � 	Resources
 5=       �� 
TargetType �� Property �� Value  AssetList_ListViewItem �� BasedOn  Layer1_Dark �� 
TargetType  Layer2_Dark   MaterialIcons.MaterialIcon  HoverBtn_Content �� Triggers P� Property P� Value��%�  '��       '�� u     7  7  '��     7  7  '  �    7  7  �� 5>      " ��6   .�5?      ��  � ��
 ���� 5@   !   �� 5A       � ��$ 0q�5B   !   5D      �� 5E      " ��6   8   .�5G      ��  � ��$ 50��5H   !   �� 5I       � ��$ 0q�5J   !   �� 5K       � ��8  5L   !   5M      �� 5O      " ��6   .�5P      ��  � ��$ 14(�5Q   !   �� 5R       � ��$ SemiBold"�5S   !   �� 5T       � ��$ 0q�5U   !   �� 5V       � �� 5W      �� 5X   "   " ��62   .R�5Y   &   � 8��  $
��-2,0q�5Z   +   .+�5[   *   ]� $
��Left=�64   $��Center=�5\   4   $
��0,10q�5]   4   .+�5^   .   �� #��z��68   $��16(�5_   8   $	��5,0q�5`   8   8�� 5a   8   5b   +   5c   '   5d   #   5f      5g      �� 5i      "  6   8    5k      P� 5l       � {� .  . �5n   "   ��  � ��8  5o   )   5p      5r      5t      .+�5v      �� -   5         ��Name$ Users��5v       �� Orientation$ Vertical=�5w      $��50q�5y      y�5x      ��  �� ElementName$ UsersBtn�� �� Path$ 	IsChecked�   BooleanToVisibilityConverter �� 	Converter# [ .+�5{      �� $��Users��6     	 RightPanel_Header#��[	 5|       �� 5      $ 
Horizontal=�6!   .+�5�      ��  
 HoverBtn#��[
 6!   $
��Left=�5�   !   $��	0,40,0,10q�5�   !    ^� ShowOnDisabled . ��5�   !   �� *
CreateUser+ ^� 	IsEnabled 5�   !   �� *	IsEnabled+ �� RelativeSource ��  �� Mode$
 Self=�  InvertBoolConverter# [ {�5�   !   ��  ��  �� AncestorLevel$ 1�� �� AncestorType$
 PageL�$$ DataContext.UserCanCreateUsers�.��5�      �� $ Vertical=�6)   $��0,0,15,0q�5�   )   .+�5�   "        Icon$ ic_add=�6=     ListHeaderBtn_Image#��[ 5�   =   �� 5�   "   $	��Add��6,     ListHeaderBtn_Text#��[ 5�   ,   5�       � ToolTip 5�      _� 5�   "   $��0q�6*   .��5�   &   �� $0��*You do not have permission to create users��60   #��[ 5�   0   5�   #   5�      �� 5�      #��[
 6!   $
��Left=�5�   !   $��	0,40,0,10q�5�   !    . ��5�   !   �� *CopySelectedUser+ 5�   !   �� $ ProjectSettingsPage��$# DataContext.UserCanCloneUsers�# [ {�5�      `� 5�   "     multiBoolAllConverter  `� 	Converter#  [ 6/   .1�5�   &   �� $ ProjectSettingsPage��6.   $# DataContext.UserCanCloneUsers�5�   .   �� 5�   &   $ ProjectSettingsPage��6.   $ DataContext.SelectedUser�5�   .     IsNullInverseConverter# [ 5�   .   5�   #   .��5�      �� $ Vertical=�6)   $��0,0,15,0q�5�   )   .+�5�   "     $ ic_content_copy=�6=   #��[ 5�   =   �� 5�   "   $
��Copy��6,   #��[ 5�   ,   5�       5�      _� 5�   "   $��0q�6*   .��5�   &   �� $.��(You do not have permission to edit users��60   #��[ 5�   0   5�   #   5�      �� 5�      #��[
 6!   $
��Left=�5�   !   $��	0,40,0,10q�5�   !    . ��5�   !   �� *EditSelectedUser+ 5�   !   �� $ ProjectSettingsPage��$" DataContext.UserCanEditUsers�# [ {�5�      `� 5�   "   #  [ 6/   .1�5�   &   �� $ ProjectSettingsPage��6.   $" DataContext.UserCanEditUsers�5�   .   �� 5�   &   $ ProjectSettingsPage��6.   $ DataContext.SelectedUser�5�   .   # [ 5�   .   5�   #   .��5�      �� $ Vertical=�6)   $��0,0,15,0q�5�   )   .+�5�   "     $ ic_edit=�6=   #��[ 5�   =   �� 5�   "   $
��Edit��6,   #��[ 5�   ,   5�       5�      _� 5�   "   $��0q�6*   .��5�   &   �� $.��(You do not have permission to edit users��60   #��[ 5�   0   5�   #   5�      �� 5�      #��[
 6!   $
��Left=�5�   !   $��	0,40,0,10q�5�   !    . ��5�   !   �� *DeleteSelectedUser+ 5�   !   �� $ ProjectSettingsPage��$$ DataContext.UserCanDeleteUsers�# [ {�5�      `� 5�   "   #  [ 6/   .1�5�   &   �� $ ProjectSettingsPage��6.   $$ DataContext.UserCanDeleteUsers�5�   .   �� 5�   &   $ ProjectSettingsPage��6.   $ DataContext.SelectedUser�5�   .   # [ 5�   .   5�   #   .��5�      �� $ Vertical=�6)   $��0,0,15,0q�5�   )   .+�5�   "     $ 	ic_delete=�6=   #��[ 5�   =   �� 5�   "   $��Delete��6,   #��[ 5�   ,   5�       5�      _� 5�   "   $��0q�6*   .��5�   &   �� $.��(You do not have permission to edit users��60   #��[ 5�   0   5�   #   5�      5�      �� 5�      -	   5        ! ��Name$! UserList��5�      " �� SelectedItem" 5�      �� *SelectedUser+# �� Mode$# TwoWay=�$ �� NotifyOnSourceUpdated$ . ��5�      �� *Users+% �� View% 5�      �� 5�      .P�5�   "   �� $��ID��61   & �� Width$	& 100��5�   1   ' �� DisplayMemberBinding' 5�   1   �� *us_id+�� 5�   "   $
��Name��61   $	& 300��5�   1   ' 5�   1   �� *us_displayname+�� 5�   "   $
��Role��61   $	& 200��5�   1   ' 5   1   �� *SecRole.ro_name+�� 5  "   $��Email��61   $	& 200��5  1   ' 5  1   �� *
us_email+5     5     5     �� 5	     -
   5        $ Roles��5	     $ Vertical=�5
     $��50q�5     y�5     �� $ RolesBtn��$ 	IsChecked�# [ .+�5     �� $��Roles��6    #��[	 5      �� 5     $ 
Horizontal=�6!   .+�5     �� #��[
 6!   $
��Left=�5  !   $��	0,40,0,10q�5  !    . ��5  !   �� *
CreateRole+ 5  !   �� *	IsEnabled+ �� $
 Self=�# [ {�5  !   ��  �� $ 1��$
 PageL�$$ DataContext.UserCanCreateRoles�.��5     �� $ Vertical=�6)   $��0,0,15,0q�5  )   .+�5  "     $ ic_add=�6=   #��[ 5  =   �� 5  "   $	��Add��6,   #��[ 5  ,   5       5!     _� 5"  "   $��0q�6*   .��5#  &   �� $0��*You do not have permission to create Roles��60   #��[ 5$  0   5%  #   5'     �� 5)     #��[
 6!   $
��Left=�5+  !   $��	0,40,0,10q�5,  !    . ��5*  !   �� *CopySelectedRole+ 5.  !   �� $ ProjectSettingsPage��$# DataContext.UserCanCloneRoles�# [ {�5/     `� 50  "   #  [ 6/   .1�51  &   �� $ ProjectSettingsPage��6.   $# DataContext.UserCanCloneRoles�52  .   �� 53  &   $ ProjectSettingsPage��6.   $ DataContext.SelectedRole�54  .   # [ 55  .   56  #   .��58     �� $ Vertical=�6)   $��0,0,15,0q�59  )   .+�5:  "     $ ic_content_copy=�6=   #��[ 5;  =   �� 5<  "   $
��Copy��6,   #��[ 5=  ,   5>      5?     _� 5@  "   $��0q�6*   .��5A  &   �� $.��(You do not have permission to edit Roles��60   #��[ 5B  0   5C  #   5E     �� 5G     #��[
 6!   $
��Left=�5I  !   $��	0,40,0,10q�5J  !    . ��5H  !   �� *EditSelectedRole+ 5L  !   �� $ ProjectSettingsPage��$" DataContext.UserCanEditRoles�# [ {�5M     `� 5N  "   #  [ 6/   .1�5O  &   �� $ ProjectSettingsPage��6.   $" DataContext.UserCanEditRoles�5P  .   �� 5Q  &   $ ProjectSettingsPage��6.   $ DataContext.SelectedRole�5R  .   # [ 5S  .   5T  #   .��5V     �� $ Vertical=�6)   $��0,0,15,0q�5W  )   .+�5X  "     $ ic_edit=�6=   #��[ 5Y  =   �� 5Z  "   $
��Edit��6,   #��[ 5[  ,   5\      5]     _� 5^  "   $��0q�6*   .��5_  &   �� $.��(You do not have permission to edit Roles��60   #��[ 5`  0   5a  #   5c     �� 5e     #��[
 6!   $
��Left=�5g  !   $��	0,40,0,10q�5h  !    . ��5f  !   �� *DeleteSelectedRole+ 5j  !   �� $ ProjectSettingsPage��$$ DataContext.UserCanDeleteRoles�# [ {�5k     `� 5l  "   #  [ 6/   .1�5m  &   �� $ ProjectSettingsPage��6.   $$ DataContext.UserCanDeleteRoles�5n  .   �� 5o  &   $ ProjectSettingsPage��6.   $ DataContext.SelectedRole�5p  .   # [ 5q  .   5r  #   .��5t     �� $ Vertical=�6)   $��0,0,15,0q�5u  )   .+�5v  "     $ 	ic_delete=�6=   #��[ 5w  =   �� 5x  "   $��Delete��6,   #��[ 5y  ,   5z      5{     _� 5|  "   $��0q�6*   .��5}  &   �� $.��(You do not have permission to edit Roles��60   #��[ 5~  0   5  #   5�     5�     �� 5�     " 6   �� *SelectedRole+$# TwoWay=�$ . ��5�     �� *Roles+% 5�     �� 5�     .P�5�  "   �� $��ID��61   $	& 100��5�  1   ' 5�  1   �� *ro_id+�� 5�  "   $
��Name��61   $	& 300��5�  1   ' 5�  1   �� *	ro_name+5�     5�     5�     �� 5�     -   5        $ Category��5�     $ Vertical=�5�     $��50q�5�     y�5�     �� $ CategoriesBtn��$ 	IsChecked�# [ .+�5�     �� $��
Categories��6    #��[	 5�      �� 5�     $ 
Horizontal=�6!   .+�5�     �� #��[
 6!   $
��Left=�5�  !   $��	0,40,0,10q�5�  !    . ��5�  !   �� *CreateCategory+ 5�  !   �� *	IsEnabled+ �� $
 Self=�# [ {�5�  !   ��  �� $ 1��$
 PageL�$) #DataContext.UserCanCreateCategories�.��5�     �� $ Vertical=�6)   $��0,0,15,0q�5�  )   .+�5�  "     $ ic_add=�6=   #��[ 5�  =   �� 5�  "   $	��Add��6,   #��[ 5�  ,   5�      5�     _� 5�  "   $��0q�6*   .��5�  &   �� $5��/You do not have permission to create Categories��60   #��[ 5�  0   5�  #   5�     �� 5�     #��[
 6!   $
��Left=�5�  !   $��	0,40,0,10q�5�  !    . ��5�  !   �� *CopySelectedCategory+ 5�  !   �� $ ProjectSettingsPage��$( "DataContext.UserCanCloneCategories�# [ {�5�     `� 5�  "   #  [ 6/   .1�5�  &   �� $ ProjectSettingsPage��6.   $( "DataContext.UserCanCloneCategories�5�  .   �� 5�  &   $ ProjectSettingsPage��6.   $" DataContext.SelectedCategory�5�  .   # [ 5�  .   5�  #   .��5�     �� $ Vertical=�6)   $��0,0,15,0q�5�  )   .+�5�  "     $ ic_content_copy=�6=   #��[ 5�  =   �� 5�  "   $
��Copy��6,   #��[ 5�  ,   5�      5�     _� 5�  "   $��0q�6*   .��5�  &   �� $3��-You do not have permission to edit Categories��60   #��[ 5�  0   5�  #   5�     �� 5�     #��[
 6!   $
��Left=�5�  !   $��	0,40,0,10q�5�  !    . ��5�  !   �� *EditSelectedCategory+ 5�  !   �� $ ProjectSettingsPage��$' !DataContext.UserCanEditCategories�# [ {�5�     `� 5�  "   #  [ 6/   .1�5�  &   �� $ ProjectSettingsPage��6.   $' !DataContext.UserCanEditCategories�5�  .   �� 5�  &   $ ProjectSettingsPage��6.   $" DataContext.SelectedCategory�5�  .   # [ 5�  .   5�  #   .��5�     �� $ Vertical=�6)   $��0,0,15,0q�5�  )   .+�5�  "     $ ic_edit=�6=   #��[ 5�  =   �� 5�  "   $
��Edit��6,   #��[ 5�  ,   5�      5�     _� 5�  "   $��0q�6*   .��5�  &   �� $3��-You do not have permission to edit Categories��60   #��[ 5�  0   5�  #   5�     �� 5�     #��[
 6!   $
��Left=�5�  !   $��	0,40,0,10q�5�  !    . ��5�  !   �� *DeleteSelectedCategory+ 5�  !   �� $ ProjectSettingsPage��$) #DataContext.UserCanDeleteCategories�# [ {�5�     `� 5�  "   #  [ 6/   .1�5�  &   �� $ ProjectSettingsPage��6.   $) #DataContext.UserCanDeleteCategories�5�  .   �� 5�  &   $ ProjectSettingsPage��6.   $" DataContext.SelectedCategory�5�  .   # [ 5   .   5  #   .��5     �� $ Vertical=�6)   $��0,0,15,0q�5  )   .+�5  "     $ 	ic_delete=�6=   #��[ 5  =   �� 5  "   $��Delete��6,   #��[ 5  ,   5	      5
     _� 5  "   $��0q�6*   .��5  &   �� $3��-You do not have permission to edit Categories��60   #��[ 5  0   5  #   5     5     �� 5     " 6   �� *SelectedCategory+$# TwoWay=�$ . ��5     �� *
Categories+% 5     �� 5     .P�5  "   �� $��ID��61   $	& 100��5  1   ' 5  1   �� *ca_id+�� 5  "   $
��Name��61   $	& 300��5  1   ' 5  1   �� *	ca_name+5#     5&     5(     �� 5*     -   5        $ Alerts��5*     $ Vertical=�5+     $��50q�5-     y�5,     �� $ 	AlertsBtn��$ 	IsChecked�# [ .+�5/     �� $��Alerts��6    #��[	 50      �� 53     #��[
 6   $
��Left=�54     $��	0,40,0,10q�55      . ��56     �� *OpenNewAlert+ 58     �� *	IsEnabled+ �� $
 Self=�# [ {�59     ��  �� $ 1��$
 PageL�$# DataContext.UserCanPostAlerts�.��5:     �� $ Vertical=�6%   $��0,0,15,0q�5;  %   .+�5<       $ ic_add=�69   #��[ 5=  9   �� 5>     $	��Add��6(   #��[ 5?  (   5@      5A     _� 5B     $��0q�6&   .��5C  "   �� $1��+You do not have permission to create alerts��6,   #��[ 5D  ,   5E     5G     �� 5I     .��5J     �� $
��0,10q�5K  '   ��5J  '   �� *ProjectWideAlerts+��5L     �� 5M  "   .R�5N  &   �� #��[ 6-   ( �� Padding$( 20q�5O  -   $��0,0,0,10q�5P  -   $	��150��5Q  -   
 5R  *   ��%V   '��       7  �� 5S  .   " ��64   .�5T  2   ��  � ��8   5U  9   5V  /   5W  +   ._�5X  *   � .+�5Y  .   �� $��0,10,0,0q�69   $	��900��5Z  9   $
��Left=�5[  9   .+�5\  2   �� $
��Left=�5]  <   $	��Top=�5^  <   ��5\  <   �� *	ar_header+5^  <   �� 5_  2   $
��Left=�5`  <   $	��Top=�5a  <   $��WrapWithOverflow=�5b  <   $
��0,20q�5c  <   ��5_  <   �� *
ar_content+5c  <   5d  /   �� 5f  .   $ 
Horizontal=�69   $��Right=�5g  9   $	��Top=�5h  9   .+�5i  2   �� #��[
 69   $	��5,0q�5j  9    . ��5k  9   ��  �� $ 1��$
 PageL�$" DataContext.EditAlertCommand���5l  9   �� *ar_id+ 5n  9   �� *	IsEnabled+ �� $
 Self=�# [ {�5o  9   ��  �� $ 1��$
 PageL�$# DataContext.UserCanEditAlerts�.��5p  6     $ ic_edit=�6Q   #��[ 5q  Q    5r  6   _� 5s  :   $��0q�6B   .��5t  >   �� $/��)You do not have permission to edit alerts��6H   #��[ 5u  H   5v  ;   5x  3   �� 5y  2   #��[
 69    . ��5z  9   ��  �� $ 1��$
 PageL�$$ DataContext.DeleteAlertCommand���5{  9   �� *ar_id+ 5}  9   �� *	IsEnabled+ �� $
 Self=�# [ {�5~  9   ��  �� $ 1��$
 PageL�$% DataContext.UserCanDeleteAlerts�.��5  6     $ 	ic_delete=�6Q   #��[ 5�  Q    5�  6   _� 5�  :   $��0q�6B   .��5�  >   �� $1��+You do not have permission to delete alerts��6H   #��[ 5�  H   5�  ;   5�  3   5�  /   �� 5�  .   $��Right=�69   $��Bottom=�5�  9   .+�5�  2   �� $��Right=�5�  <   ��5�  <   �� *User.us_displayname+5�  <   �� 5�  2   ��6<   �� *	ar_date+5�  /   5�  +   5�  '   5�  #   5�     5�     5�     �� 5�     -   5        $ AddAlertPanel��5�       Overlay#��[ 5�     $��Center=�5�     $��Center=�5�     $( 30q�5�     y�5�     �� *PromptNewAlert+# [ ._�5�     �� .+�5�     �� $��Center=�6$   $��Add Project-Wide Alert��5�  $   $��5,0,0,0q�5�  $   $��16(�5�  $   $��SemiBold"�5�  $   y�5�  $   �� *AlertPrompt_ID+  IsNullToVizConverter# [ �� 5�     $��Center=�6$   $��Edit Project-Wide Alert��5�  $   $��5,0,0,0q�5�  $   $��16(�5�  $   $��SemiBold"�5�  $   y�5�  $   �� *AlertPrompt_ID+  IsNullInvertToVizConverter# [ �� 5�     $��
20,20,20,0q�6%   .+�5�     �� $��Header��6(   $��0,0,0,10q�5�  (   �� 5�     -   5        ) ��Name$) NewAlert_Header��5�  &   $	��400��5�  &   $��30��5�  &   $��Center=�5�  &   ��5�  &   �� *AlertPrompt_Header+�� 5�     $��Contents��6(   $��	0,30,0,10q�5�  (   �� 5�     -   5        $) NewAlert_Contents��5�  &   $	��400��5�  &   $	��200��5�  &   * �� TextWrapping$* WrapWithOverflow=�5�  &   ��5�  &   �� *AlertPrompt_Contents+5�  &   �� 5�     $ 
Horizontal=�6)   $��Right=�5�  )   .+�5�  "   �� #��[
 6*     RejectOverlay_Dark#��[ 5�  *   $	��100��5�  *   $��30��5�  *   $��0,20,0,0q�5�  *   ��5�  *   �� *CloseNewAlert+.��5�  &   �� $��Cancel��60     HoverBtn_TextContent#��[ 5�  0   $��Center=�5�  0   $��Center=�5�  0   5�  #   �� 5�  "   #��[
 6)     ConfirmOverlay_Dark#��[ 5�  )   $	��100��5�  )   $��30��5�  )   $��	20,20,0,0q�5�  )   ��5�  )   �� *CreateAlert+.��5�  &   �� $
��Send��60   #��[ 5�  0   $��Center=�5�  0   $��Center=�5�  0   5�  #   5�     5�     5�     5�     �� 5�     -   5        $ DeletePrompt��5�     #��[ 5�     $��Center=�5�     $��Center=�5�     $( 30q�5�     y�5�     �� *PromptDelete+# [ ._�5�     �� .+�5�     �� $ 
Horizontal=�6%   .+�5�       $ ic_delete_forever=�69     TextOverlay_Dark#��[ 5�  9   �� 5�     $��Delete Asset��6(   $��Center=�5�  (   5�     �� 5�     $��Center=�6$   $
��0,50q�5�  $   $��16(�5�  $   �� 5�     $%�Are you sure you want to delete��6    �� 5�     $
��Bold"�5�     �5�     �� *DeletionObject.Name+5�      �� 5�     $�#��6   $
��Bold"�5�      �� 5�     $
��Bold"�5�     �5�     �� *DeletionObject.ID+5�      �� 5�     $�?��6   5�     �� 5�     $ 
Horizontal=�6%   $��Center=�5�  %   .+�5�     �� $��	0,10,20,0q�6%   #��[
 5�  %   $��5q�5�  %   $��5q�5   %   #��[ 5  %   $	��150��5  %   ��5  %   �� *DeleteConfirmed+.��5  "   �� $��Confirm��6,   #��[ 5  ,   $��Center=�5  ,   $��Center=�5  ,   #��[ 5  ,   $��SemiBold"�5	  ,   $��14(�5
  ,   5     �� 5     $��	0,10,20,0q�6&   $	��150��5  &   #��[
 5  &   $��5q�5  &   $��5q�5  &   #��[ 5  &   ��5  &   �� *DeleteCanceled+.��5  "   �� $��Cancel��6,   #��[ 5  ,   $��Center=�5  ,   $��Center=�5  ,   #��[ 5  ,   $��SemiBold"�5  ,   $��14(�5  ,   5     5     5     5     5!     5"     5#     5$     