# OpenFlows Sewers - Unitify Scenarios, Alternatives, and Calc Options

Delete **"non-active"** scenarios, alternatives, and calc options to reduce the clutter and start fresh. Note: The other components, like patterns, pump curves etc. are **NOT** deleted part of this.

## ⬇️ Download

Please read setup, please 🙏.
> [StormSewer 10.3.4.x](https://github.com/worthapenny/OpenFlows-Sewer--UnitifySAC/releases/download/v10.3.4.0/OFS.UnitifySAC_10.3.4.00.7z)

## ⚙️Setup

1. [Admin privilege is needed]
2. Extract the downloaded contents to the **x64** directory of the installed directory, typically, `C:\Program Files (x86)\Bentley\SewerGEMS\x64`.
3. **OFW.UnitifySAC** can be executed from above location. If executed from other than above location, application may not launch at all.

## 🔨 How it works

On a high level:

1. Deletes the non-active scenarios without deleting the branch that is part of active scenario path.
2. Deletes the non-active alternatives without deleting the branch that is part of the active alternative path.
3. Deletes the non-active calculation options.
4. Merges the alternaties from deepest level all the way to the base level
5. Cleans up the scenario tree if needed until only one scenario is left

## 😎 How to use

Once the application is placed along with the main Bentley StormSewer application in the **x64** directory, double click the **OFS.UnitifySAC** to launch the application. A window like below should show.

![Main Application Window](https://github.com/worthapenny/OpenFlows-Sewer--UnitifySAC/blob/main/Images/MainAppWindow.png "Main Application Window")

1. Click on folder icon 📁 to select a StormSewer file for the clean-up. Once selected, model will open internally. A dialog-box will be displayed when open process is completed.
2. Click on **Unitify SAC** icon to initiate the clean-up process
3. Once the clean up is completed, either click on **Save** or **Save As** to save the changes.
4. To view the changes in the main StormSewer application, click on the Bentley 🇧 icon.
5. The **Unitify SAC** application window can now be closed.

## 🗩 Comments, Questions, Issues, Suggestions, ...

Please create and issue in the GitHub or drop me an email. Thank you!

## 🙏 Thank you

* **Danielle Orgill** and **Brett O'Hair**: For inspiring me about the tool
* **Kris Culin**: As always for the great help and support.

## 📜   Major update history

* April 28 2022: Initial Post

## 🛠️ Other projects based on OpenFlows and/or WaterObjects.NET

* [Isolation Valve Adder](https://github.com/worthapenny/OpenFlows-Water--IsolationValveAdder)
* [Bing Background Adder](https://github.com/worthapenny/OpenFlows-Water--BingBackground)
* [Model Merger](https://github.com/worthapenny/OpenFlows-Water--ModelMerger)
* [Demand to CustomerMeter](https://github.com/worthapenny/OpenFlows-Water--DemandToCustomerMeter)

## 💡 Did you know?

Now, you can work with Bentley OpenFlows Water products with python as well. Check out:

* [Github pyofw](https://github.com/worthapenny/pyofw)
* [PyPI pyofw](https://pypi.org/project/pyofw/)

![pypi-image](https://github.com/worthapenny/OpenFlows-Water--ModelMerger/blob/main/images/pypi_pyofw.png "pyOFW module on pypi.org")