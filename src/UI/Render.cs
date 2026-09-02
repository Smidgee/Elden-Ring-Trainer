using ClickableTransparentOverlay;
using ImGuiNET;
using System.Numerics;

namespace EldenRingTrainer
{
    public class RenderUI : Overlay
    {
        private bool IsStyleInitialized = false;
        private bool IsVisible = true;

        private readonly RunesService _runesService;
        private readonly StatsService _statsService;
        private readonly HealthService _healthService;

        string[] WeightOptions = { "Light Weight", "Medium Weight", "Heavy Charge", "OverLoaded" };

        public RenderUI(RunesService runesService, StatsService statsService, HealthService healthService) : base(2560, 1440)
        {
            _runesService = runesService;
            _statsService = statsService;
            _healthService = healthService;
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
                }

                if (ImGui.BeginTabBar("Main"))
                {
                    if (ImGui.BeginTabItem("Player"))
                    {
                        if (ImGui.CollapsingHeader("Health"))
                        {
                            ImGui.PushItemWidth(40);

                            ImGui.InputInt("Health Amount", ref _healthService.Health, 0);

                            if (ImGui.Button("Set Health"))
                            {
                                _healthService.SetHP(_healthService.Health);
                            }
                            ImGui.PopItemWidth();
                        }
                        if (ImGui.CollapsingHeader("Stats"))
                        {
                            ImGui.PushItemWidth(25);

                            ImGui.InputInt("Vigor", ref _statsService.Vigor, 0);
                            ImGui.InputInt("Mind", ref _statsService.Mind, 0);
                            ImGui.InputInt("Endurance", ref _statsService.Endurance, 0);
                            ImGui.InputInt("Strength", ref _statsService.Strength, 0);
                            ImGui.InputInt("Dexterity", ref _statsService.Dexterity, 0);
                            ImGui.InputInt("Intelligence", ref _statsService.Intelligence, 0);
                            ImGui.InputInt("Faith", ref _statsService.Faith, 0);
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
                                _statsService.FixWeightObscruction();
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

                        if (ImGui.Button("Add Rune"))
                        {
                            _runesService.AddRune(_runesService.AddRuneAmount);
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