   M S B A M L   `   `   `  ����C  ?WpfApp12, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:WpfApp12WpfApp12  5   	   #clr-namespace:WpfApp12WpfApp12  5   	   #clr-namespace:WpfApp12WpfApp12  5   	       WpfApp12.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   (localclr-namespace:WpfApp12     5   	   %vmclr-namespace:WpfApp12     5   	   -
convertersclr-namespace:WpfApp12     5   	     � Title$  Bus Express��5   	   $	��600��6   $	��800��6*    � 	Resources 5	         WpfApp12.TupleConverter   TupleConverter(   "WpfApp12.TextToVisibilityConverter  TextToVisibilityConverter#   WpfApp12.TextToColorConverter  TextToColorConverter&    WpfApp12.BooleanToColorConverter  BooleanToColorConverter��%t   &	        &	      &	 &     &	 9       5
   
   6$     5   
   6/     5   
   6*     5   
   6-   5       � DataContext 5         WpfApp12.BusStopViewModel  5   
   .��5      �� $
��Auto=�6   .��5      �� .+�5   
   �� $��Bus Stop Simulation��6   $��20(�6.   $��Center=�6<   $��10q�6Y   �� 5   
   $��Start Simulation��6   $��5q�6W   ���	#FF4EC54B��6,   �� *StartSimulationCommand+6b   �� 5   
   $��Stop Simulation��6   $��5q�6U   ���	#FFFF6C6C��6+   �� *StopSimulationCommand+6`   �� 5   
   $��Add New Bus Stop��6   $��5q�6R   ��6,   �� *AddBusStopCommand+6R   �� 5   
   $��Add New Bus��6   $��5q�6I   ��6'   �� *AddBusCommand+6I   �� 5   
   $��5q�63   ��6   �� *Stops+��5      �� 5      .R�5      �� ��6    �� *StopId+5        �� ContentTemplate 5"      �� 5#      .R�5$      �� ��6   �� *Buses+��5%      �� 5&      .R�5'   "   �� .+�5*   &   �� $
��Bold"�6D   $��14(�6V   ��60   �� *Id+6V   �� 5+   &   ��60   �� *Status+��6H   �� *Status+ �� 	Converter# [ �� 5,   &   $��Overcrowded:��60   �� 5-   &   ��60   �� *IsOvercrowded+��6O   �� *IsOvercrowded+# [ �� 5.   &   $��Passengers Count:��60   �� 5/   &   ��60   �� *CurrentPassengerCount+ �� UpdateSourceTrigger$ PropertyChanged=��� 50   &   $��Passengers:��60   �� 51   &   ��6/   �� *
Passengers+��52   *   �� 53   .   .R�54   2   �� ��6<   �� *Name+55   /   57   '   58   #   59      5;      5<      5>      5?      5@      5A      