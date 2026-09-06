using ClickableTransparentOverlay;
using ImGuiNET;
using System.Numerics;

namespace EldenRingTrainer
{
    public class RenderUI : Overlay
    {
        private bool IsStyleInitialized = false;
        private bool IsVisible = true;
        private bool IsHealthFreezed = false;
        private bool IsFPFreezed = false;
        private bool IsEnduranceFreezed = false;

        private readonly RunesService _runesService;
        private readonly StatsService _statsService;
        private readonly HealthService _healthService;
        private readonly FPService _fpService;
        private readonly EnduranceService _enduranceService;

        string[] WeightOptions = { "Light Weight", "Medium Weight", "Heavy Charge", "OverLoaded"};

        public RenderUI(RunesService runesService, StatsService statsService, HealthService healthService, FPService fpService, EnduranceService enduranceService) : base(2560, 1440)
        {
            _runesService = runesService;
            _statsService = statsService;
            _healthService = healthService;
            _fpService = fpService;
            _enduranceService = enduranceService;
        }

        protected override void Render()
        {
            ImGui.SetNextWindowSize(new Vector2(700, 400), ImGuiCond.Once);

            if (ImGui.IsKeyPressed(ImGuiKey.F11) && IsVisible)
            {
                IsVisible = false;

            } else if (ImGui.IsKeyPressed(ImGuiKey.F11) && !IsVisible)
            {
                IsVisible = true;
            }

            if (IsVisible)
            {

                ImGui.Begin("Elden Ring Trainer");

                if (!IsStyleInitialized)
                {
                    Style();
                    IsStyleInitialized = true;
                    _statsService.GetCurrentStat();
                    _healthService.GetCurrentHP();
                    _fpService.GetCurrentFP();
                    _enduranceService.GetCurrentEndurance();
                }

                if (ImGui.BeginTabBar("Main"))
                {
                    if (ImGui.BeginTabItem("Player"))
                    {
                        if (ImGui.CollapsingHeader("Health"))
                        {
                            ImGui.PushItemWidth(40);

                            ImGui.InputInt("Health Amount", ref _healthService.Health, 0);

                            ImGui.SameLine();

                            if (IsHealthFreezed == false)
                            {
                                if (ImGui.Button("Set Health"))
                                {
                                    _healthService.SetHP(_healthService.Health);
                                }
                            } else
                            {
                                ImGui.Button("! Restore to Set Health !");
                            }

                            if (ImGui.Button("No Damage"))
                            {
                                _healthService.NoDamage();
                                IsHealthFreezed = true;
                            }

                            ImGui.SameLine();

                            if (ImGui.Button("Restore Damage"))
                            {
                                _healthService.RestoreDamage();
                                IsHealthFreezed = false;
                            }

                            ImGui.PopItemWidth();
                        }
                        if (ImGui.CollapsingHeader("FP"))
                        {
                            ImGui.PushItemWidth(40);

                            ImGui.InputInt("FP Amount", ref _fpService.FP, 0);

                            ImGui.SameLine();

                            if (IsFPFreezed == false)
                            {
                                if (ImGui.Button("Set FP"))
                                {
                                    _fpService.SetFP(_fpService.FP);
                                }
                            } else
                            {
                                ImGui.Button("! Unfreeze to Sets FP !");
                            }

                            if (ImGui.Button("Freeze FP"))
                            {
                                _fpService.FreezeFP();
                                IsFPFreezed = true;
                            }

                            ImGui.SameLine();

                            if (ImGui.Button("UnFreeze FP"))
                            {
                                _fpService.UnFreezeFP();
                                IsFPFreezed = false;
                            }

                            ImGui.PopItemWidth();
                        }

                        if (ImGui.CollapsingHeader("Endurance"))
                        {
                            ImGui.PushItemWidth(40);

                            ImGui.InputInt("Endurance Amount", ref _enduranceService.Endurance, 0);

                            ImGui.SameLine();

                            if (IsEnduranceFreezed == false)
                            {
                                if (ImGui.Button("Set Endurance"))
                                {
                                    _enduranceService.SetEndurance(_enduranceService.Endurance);

                                }
                            } else 
                            {
                                ImGui.Button("! Unfreeze to Set Endurance !");
                            }

                            if (ImGui.Button("Freeze Endurance"))
                            {
                                _enduranceService.FreezeEndurance();
                                IsEnduranceFreezed = true;
                            }

                            ImGui.SameLine();

                            if (ImGui.Button("Unfreeze Endurance"))
                            {
                                _enduranceService.UnFreezeEndurance();
                                IsEnduranceFreezed = false;
                            }

                            ImGui.PopItemWidth();
                        }
                        if (ImGui.CollapsingHeader("Stats"))
                        {
                            ImGui.PushItemWidth(25);

                            ImGui.InputInt("Vigor", ref _statsService.Vigor, 0);
                            ImGui.SameLine();
                            ImGui.InputInt("Mind", ref _statsService.Mind, 0);
                            ImGui.SameLine();
                            ImGui.InputInt("Endurance", ref _statsService.Endurance, 0);
                            ImGui.SameLine();
                            ImGui.InputInt("Strength", ref _statsService.Strength, 0);
                            ImGui.InputInt("Dexterity", ref _statsService.Dexterity, 0);
                            ImGui.SameLine();
                            ImGui.InputInt("Intelligence", ref _statsService.Intelligence, 0);
                            ImGui.SameLine();
                            ImGui.InputInt("Faith", ref _statsService.Faith, 0);
                            ImGui.SameLine();
                            ImGui.InputInt("Arcane", ref _statsService.Arcane, 0);

                            ImGui.PopItemWidth();

                            if (ImGui.Button("Set Stats"))
                            {
                                _statsService.SetStats(_statsService.Vigor, _statsService.Mind, _statsService.Endurance, _statsService.Strength, _statsService.Dexterity, _statsService.Intelligence, _statsService.Faith, _statsService.Arcane);
                            }
                        }
                        if (ImGui.CollapsingHeader("Weight"))
                        {
                            ImGui.PushItemWidth(100);

                            if (ImGui.ListBox("##WeightOptions", ref _statsService.Weight, WeightOptions, 4))
                            {
                                _statsService.FixWeightObstruction();
                                _statsService.SetWeight(_statsService.Weight);

                            }
                            ImGui.PopItemWidth();
                        }
                        ImGui.EndTabItem();
                    }
                    if (ImGui.BeginTabItem("Misc"))
                    {
                        ImGui.PushItemWidth(75);

                        ImGui.InputInt("Rune Amount", ref _runesService.AddRuneAmount, 0);

                        ImGui.PopItemWidth();

                        ImGui.SameLine();

                        if (ImGui.Button("Add Rune"))
                        {
                            _runesService.AddRune(_runesService.AddRuneAmount);
                        }

                        if (ImGui.Button("Freeze Rune"))
                        {
                            _runesService.FreezeRune();
                        }

                        ImGui.SameLine();

                        if (ImGui.Button("Unfreeze Rune"))
                        {
                            _runesService.UnFreezeRune();
                        }

                        ImGui.EndTabItem();
                    }
                    ImGui.EndTabBar();
                }
                ImGui.End();
            }
        }

        private void Style()
        {
            var style = ImGui.GetStyle();

            style.WindowRounding = 8.0f;
            style.FrameRounding = 4.0f;
            style.TabRounding = 4.0f;

            style.FramePadding = new Vector2(6, 4);
            style.ItemSpacing = new Vector2(10, 8);

            var color = style.Colors;

            color[(int)ImGuiCol.WindowBg] = new Vector4(0.05f, 0.05f, 0.05f, 0.9f);
            color[(int)ImGuiCol.Border] = new Vector4(0.15f, 0.15f, 0.15f, 0.95f);
            color[(int)ImGuiCol.FrameBg] = new Vector4(0.15f, 0.15f, 0.15f, 0.95f);
            color[(int)ImGuiCol.TitleBgActive] = new Vector4(0.15f, 0.15f, 0.15f, 0.95f);
            color[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.15f, 0.15f, 0.15f, 0.95f);
            color[(int)ImGuiCol.Header] = new Vector4(0.15f, 0.15f, 0.15f, 0.95f);
            color[(int)ImGuiCol.HeaderHovered] = new Vector4(0.3f, 0.3f, 0.3f, 0.95f);
            color[(int)ImGuiCol.HeaderActive] = new Vector4(0.5f, 0.5f, 0.5f, 0.95f);
            color[(int)ImGuiCol.Text] = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            color[(int)ImGuiCol.TabSelected] = new Vector4(0.1f, 0.1f, 0.1f, 0.95f);
            color[(int)ImGuiCol.TabSelectedOverline] = new Vector4(0.15f, 0.15f, 0.15f, 0.95f);
            color[(int)ImGuiCol.TabHovered] = new Vector4(0.45f, 0.45f, 0.45f, 0.95f);
            color[(int)ImGuiCol.Tab] = new Vector4(0.15f, 0.15f, 0.15f, 0.95f);
            color[(int)ImGuiCol.Button] = new Vector4(0.15f, 0.15f, 0.15f, 0.95f);
            color[(int)ImGuiCol.ButtonHovered] = new Vector4(0.3f, 0.3f, 0.3f, 0.95f);
            color[(int)ImGuiCol.ButtonActive] = new Vector4(0.5f, 0.5f, 0.5f, 0.95f);
            color[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.5f, 0.5f, 0.5f, 0.95f);
            color[(int)ImGuiCol.ResizeGrip] = new Vector4(0.5f, 0.5f, 0.5f, 0.95f);
            color[(int)ImGuiCol.ResizeGripHovered]  = new Vector4(0.65f, 0.65f, 0.65f, 0.95f);
            color[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.8f, 0.8f, 0.8f, 0.95f);
            color[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.5f, 0.5f, 0.5f, 0.5f);
            color[(int)ImGuiCol.SeparatorActive] = new Vector4(0.6f, 0.6f, 0.6f, 0.95f);

        }
    }

}